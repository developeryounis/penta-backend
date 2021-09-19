using Penta.Service.Models;
using Penta.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Services.Interface
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> Login(LoginViewModel loginViewModel);
    }
}
