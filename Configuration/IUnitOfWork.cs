using System.Threading.Tasks;
using TestAPI.Repos.Interfaces;

namespace TestAPI.Configuration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}