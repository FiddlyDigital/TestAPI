using System.Threading.Tasks;
using TestAPI.DAL.Repos.Interfaces;

namespace TestAPI.DAL.Configuration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}