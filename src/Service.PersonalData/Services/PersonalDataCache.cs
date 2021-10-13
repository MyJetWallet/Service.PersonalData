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
        private readonly Dictionary<string, string> _userIDsByPhone = new();
        private readonly Dictionary<string, string> _userIDsByEmail = new();
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
                if(!string.IsNullOrEmpty(entity.Email))
                    _userIDsByEmail.TryAdd(entity.Email, entity.Id);
                if(!string.IsNullOrEmpty(entity.Phone))
                    _userIDsByPhone.TryAdd(entity.Phone, entity.Id);
            }
            
            _logger.LogInformation("Initial PD cache population finished");
        }

        public string GetUserIdByPhone(string phone)
        {
            _userIDsByPhone.TryGetValue(phone, out var id);
            return id;
        }
        
        public string GetUserIdByEmail(string email)
        {
            _userIDsByEmail.TryGetValue(email, out var id);
            return id;
        }

        public void UpdateCache(IPersonalData personalData)
        {
            _logger.LogInformation("Updating PD cache");
            if(!string.IsNullOrEmpty(personalData.Email) && !_userIDsByEmail.TryAdd(personalData.Email, personalData.Id)) 
                _userIDsByEmail[personalData.Email] = personalData.Id;
            if(!string.IsNullOrEmpty(personalData.Phone) && !_userIDsByPhone.TryAdd(personalData.Phone, personalData.Id))
                _userIDsByPhone[personalData.Phone] = personalData.Id;
        }

        public List<string> Search(string searchText)
        {
            var resultEmail = _userIDsByEmail.Where(t => t.Key.Contains(searchText)).Select(t => t.Value).ToList();
            var resultPhone = _userIDsByPhone.Where(t => t.Key.Contains(searchText)).Select(t => t.Value).ToList();
            return resultEmail.Concat(resultPhone).ToList();
        }
    }
}