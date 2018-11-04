using Turshia.Models;
using SIS.MvcFramework;

namespace Turshia.Services
{
    public interface IUserService
    {
        bool RegisterUser(RegisterViewModel model);
        bool UserExists(LoginViewModel model);
        MvcUserInfo GetUserInfo(string username);
    }
}