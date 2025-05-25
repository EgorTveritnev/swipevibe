using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLoginResult Login(UserLoginData creds);
        UserRegisterResult Register(UserRegisterData dto);
        void Logout(int userId);

    }
}