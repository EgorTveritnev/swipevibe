using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic.Interfaces
{

    public interface IUserService
    {
        IEnumerable<UserReturn> GetAllUsers();
        UserReturn GetUserById(int id);
        UserReturn GetUserByUsername(string username);
        UserReturn GetUserByEmail(string email);

        UserReturn Authenticate(string email, string password);
        UserReturn Register(UserRegister dto);
        string GeneratePasswordResetCode(string email);
        bool ResetPassword(string email, string code, string newPassword);
        void ToggleUserStatus(int id);
        int GetUsersCount();
        int GetActiveUsersCount();
        int GetBlockedUsersCount();
        int GetNewUsersToday();
    }
}
