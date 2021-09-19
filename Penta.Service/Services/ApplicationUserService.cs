using Penta.Service.Models;
using Penta.Service.Repositories.Interface;
using Penta.Service.Services.Interface;
using Penta.Service.ViewModels;
using System.Threading.Tasks;

namespace Penta.Service.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        public ApplicationUserService(IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<ApplicationUser> Login(LoginViewModel loginViewModel)
        {
            var mappedUser = new ApplicationUser() { Username = loginViewModel.Username, Password = loginViewModel.Password };

            return await _applicationUserRepository.Login(mappedUser);
        }
    }
}
