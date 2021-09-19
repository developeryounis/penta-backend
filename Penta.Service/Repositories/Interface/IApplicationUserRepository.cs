using Penta.Service.Models;
using System.Threading.Tasks;

namespace Penta.Service.Repositories.Interface
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> Login(ApplicationUser user);
    }
}
