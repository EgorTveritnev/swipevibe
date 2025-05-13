using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{
    public interface IUser
    {
        int Register(UserRegister dto);
        UserReturn Login(UserLogin dto);
        void Logout(int userId);

        UserReturn GetById(int id);
        IEnumerable<UserReturn> GetAll();

        void UpdateProfile(int id, UserUpdate dto);
    }
}
