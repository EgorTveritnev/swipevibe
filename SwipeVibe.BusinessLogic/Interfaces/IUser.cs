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

            UserReturn Authenticate(string email, string password); // используется при логине
            void Logout(int userId); // просто очищает сессию

            UserReturn GetById(int id);
            IEnumerable<UserReturn> GetAllUsers();

            void UpdateProfile(int id, UserUpdate dto);
        }
    }