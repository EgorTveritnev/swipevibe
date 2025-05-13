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
        // Чтение
        IEnumerable<UserReturn> GetAllUsers();
        UserReturn GetUserById(int id);
        UserReturn GetUserByUsername(string username);
        UserReturn GetUserByEmail(string email);

        // CRUD / auth
        UserReturn Authenticate(string email, string password);
        UserReturn Register(UserRegister dto);
        string GeneratePasswordResetCode(string email);
        bool ResetPassword(string email, string code, string newPassword);

        // Администрирование
        void ToggleUserStatus(int id);

        // Статистика
        int GetUsersCount();
        int GetActiveUsersCount();
        int GetBlockedUsersCount();
        int GetNewUsersToday();
    }
}
