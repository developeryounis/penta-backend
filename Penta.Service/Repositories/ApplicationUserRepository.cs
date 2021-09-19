using Microsoft.EntityFrameworkCore;
using Penta.Service.Models;
using Penta.Service.Repositories.Interface;
using System.Threading.Tasks;

namespace Penta.Service.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public async Task<ApplicationUser> Login(ApplicationUser user)
        {
            using (var db = new PentaEntaties())
            {
                return await db.Users
                    .Include(i => i.Privileges)
                    .ThenInclude(c => c.Privilege)
                    .FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
            }
        }
    }
}
