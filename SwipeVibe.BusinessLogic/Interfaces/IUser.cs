using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SwipeVibe.Domain.Entities.User;

    namespace SwipeVibe.BusinessLogic.Interfaces
    {        public interface IUser
        {
            int Register(UserRegister dto);

            UserReturn Authenticate(string email, string password); 
            void Logout(int userId); 

            UserReturn GetById(int id);
            IEnumerable<UserReturn> GetAllUsers();

            void UpdateProfile(int id, UserUpdate dto);
            void UpdateAvatar(int userId, string avatarUrl);
            void GeneratePasswordResetCode(string email);
        }
    }