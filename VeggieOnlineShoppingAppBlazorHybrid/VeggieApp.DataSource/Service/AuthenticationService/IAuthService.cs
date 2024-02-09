using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Authentication;

namespace VeggieApp.DataSource.Service.AuthenticationService
{
    public interface IAuthService
    {
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<LogInResult> Login(LogInModel loginModel);

        Task Logout();
    }
}
