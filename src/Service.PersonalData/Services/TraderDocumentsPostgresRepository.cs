using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;
using Service.PersonalData.Postgres;
using Service.PersonalData.Postgres.Models;

namespace Service.PersonalData.Services
{
    public class TraderDocumentsPostgresRepository
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

        public const string Table = "documents";

        private readonly ILogger<TraderDocumentsPostgresRepository> _logger;

        public TraderDocumentsPostgresRepository(ILogger<TraderDocumentsPostgresRepository> logger, DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public async Task<IEnumerable<TraderDocument>> GetDocumentsAsync(string traderId)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                return ctx.TraderDocuments.Where(t=>t.TraderId == traderId && !t.IsDeleted);
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        
        public async Task<TraderDocument> GetDocumentsAsync(string traderId, string documentId)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                return await ctx.TraderDocuments.FirstOrDefaultAsync(t=>t.TraderId == traderId && t.Id == documentId && !t.IsDeleted);
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async ValueTask Add(TraderDocument itm)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                await ctx.TraderDocuments.AddAsync(itm);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async ValueTask DeleteAsync(string traderId, string docId)
        {
            try
            {
                await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
                var entity =
                    await ctx.TraderDocuments.FirstOrDefaultAsync(t => t.TraderId == traderId && t.Id == docId);

                entity.IsDeleted = true;
                
                ctx.TraderDocuments.Update(entity);
                await ctx.SaveChangesAsync();
            }
            catch (NpgsqlException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}