using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestAPI.Configuration;
using TestAPI.Repos;
using TestAPI.Repos.Interfaces;

namespace TestAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}