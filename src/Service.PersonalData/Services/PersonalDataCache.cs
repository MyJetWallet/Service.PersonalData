using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.PersonalData.Domain.Models;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Services
{
    public class PersonalDataCache 
    {
        private readonly Dictionary<string, List<string>> _userIDsByPhone = new();
        private readonly Dictionary<string, List<string>> _userIDsByEmail = new();
        private readonly Dictionary<string, List<string>> _userIDsByNames = new();
        private readonly ILogger<PersonalDataCache> _logger;

        private readonly PersonalDataPostgresRepository _personalDataPostgresRepository;

        public PersonalDataCache(PersonalDataPostgresRepository personalDataPostgresRepository, ILogger<PersonalDataCache> logger)
        {
            _personalDataPostgresRepository = personalDataPostgresRepository;
            _logger = logger;
        }

        public async Task Start()
        {
            _logger.LogInformation("Started populating pd cache");
            var limit = 500;
            var offset = 0;
            var entities = new List<PersonalDataPostgresEntity>();
            List<PersonalDataPostgresEntity> response;
            do
            {
                response = (await _personalDataPostgresRepository.GetAsync(limit, offset,
                    Program.EncodingKey)).ToList();

                entities.AddRange(response);
                offset += response.Count;
                limit += response.Count;

            } while (response.Any());

            foreach (var entity in entities)
            {
                if (!string.IsNullOrEmpty(entity.Email))
                {
                    if (!_userIDsByEmail.ContainsKey(entity.Email))
                        _userIDsByEmail[entity.Email] = new List<string>();
                    _userIDsByEmail[entity.Email].Add(entity.Id);
                }
                if (!string.IsNullOrEmpty(entity.Phone))
                {
                    if (!_userIDsByPhone.ContainsKey(entity.Phone))
                        _userIDsByPhone[entity.Phone] = new List<string>();
                    _userIDsByPhone[entity.Phone].Add(entity.Id);
                }
                if (!string.IsNullOrEmpty(entity.FirstName) || !string.IsNullOrEmpty(entity.LastName))
                {
                    var names = $"{entity.FirstName ?? ""} {entity.LastName ?? ""}";
                    
                    if (!_userIDsByNames.ContainsKey(names))
                        _userIDsByNames[names] = new List<string>();
                    _userIDsByNames[names].Add(entity.Id);
                }
            }
            
            _logger.LogInformation("Initial PD cache population finished");
        }

        public List<string> GetUserIdsByPhone(string phone)
        {
            return _userIDsByPhone.TryGetValue(phone, out var ids) ? ids : new List<string>();
        }
        
        public string GetUserIdByEmail(string email)
        {
            if (_userIDsByEmail.TryGetValue(email, out var ids))
            {
                if (ids.Count() > 1)
                {
                    _logger.LogError($"Cannot get PersonalData by email {email}, because exist {ids.Count()} records");
                    throw new Exception($"Cannot get PersonalData by email, because exist {ids.Count()} records");
                }

                return ids.FirstOrDefault();
            }
            
            return string.Empty;
        }

        public void UpdateCache(IPersonalData personalData)
        {
            _logger.LogInformation("Updating PD cache");
            
            foreach (var item in _userIDsByPhone)
            {
                item.Value.Remove(personalData.Id);
            }
            foreach (var item in _userIDsByEmail)
            {
                item.Value.Remove(personalData.Id);
            }
            foreach (var item in _userIDsByNames)
            {
                item.Value.Remove(personalData.Id);
            }
            
            if (!string.IsNullOrEmpty(personalData.Email))
            {
                if (!_userIDsByEmail.ContainsKey(personalData.Email))
                    _userIDsByEmail[personalData.Email] = new List<string>();
                
                if (!_userIDsByEmail[personalData.Email].Contains(personalData.Id))
                    _userIDsByEmail[personalData.Email].Add(personalData.Id);
            }
            
            if (!string.IsNullOrEmpty(personalData.Phone))
            {
                if (!_userIDsByPhone.ContainsKey(personalData.Phone))
                    _userIDsByPhone[personalData.Phone] = new List<string>();
                
                if (!_userIDsByPhone[personalData.Phone].Contains(personalData.Id))
                    _userIDsByPhone[personalData.Phone].Add(personalData.Id);
            }
            
            var names = $"{personalData.FirstName ?? ""} {personalData.LastName ?? ""}";
            if (!string.IsNullOrEmpty(names))
            {
                if (!_userIDsByNames.ContainsKey(names))
                    _userIDsByNames[names] = new List<string>();
                
                if (!_userIDsByNames[names].Contains(personalData.Id))
                    _userIDsByNames[names].Add(personalData.Id);
            }
        }

        public List<string> Search(string searchText)
        {
            var resultEmail = _userIDsByEmail.Where(t => t.Key.Contains(searchText)).SelectMany(t => t.Value).ToList();
            var resultPhone = _userIDsByPhone.Where(t => t.Key.Contains(searchText)).SelectMany(t => t.Value).ToList();
            var resultNames = _userIDsByNames.Where(t => t.Key.Contains(searchText)).SelectMany(t => t.Value).ToList();
            
            return resultEmail.Concat(resultPhone).Concat(resultNames).Distinct().OrderBy(e => e).ToList();
        }
    }
}