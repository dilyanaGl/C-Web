using Chushka.Models;
using SIS.MvcFramework;

namespace Chushka.Services
{
    public interface IUserService
    {
        bool RegisterUser(RegisterViewModel model);
        bool UserExists(LoginViewModel model);
        MvcUserInfo GetUserInfo(string username);
    }
}