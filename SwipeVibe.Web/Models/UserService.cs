using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.Web.Models
{
    /// <summary>
    /// Простейшая in-memory реализация IUserService для ASP.NET MVC.
    /// В продакшене этот список легко заменить на работу с БД.
    /// </summary>
    public class UserService : IUserService
    {
        #region внутренняя модель (только для хранилища)

        private sealed class UserInternal
        {
            public int      Id;
            public string   Username;
            public string   Email;
            public string   PasswordHash;
            public string AvatarUrl;
            public Role     Role;
            public bool     IsBlocked;
            public DateTime CreatedAt;
            public DateTime? LastLogin;

            public string   ResetPasswordCode;
            public DateTime? ResetPasswordCodeExpiration;
        }

        #endregion

        #region демо-данные

        private static readonly List<UserInternal> _users = new List<UserInternal>
        {
            new UserInternal
            {
                Id = 1,
                Username = "demo",
                Email = "demo@swipevibe.com",
                PasswordHash = HashPassword("password"),
                Role = Role.User,
                IsBlocked = false,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                AvatarUrl = "/Content/Images/avatars/default.png"
            },
            new UserInternal
            {
                Id = 2,
                Username = "admin",
                Email = "admin@swipevibe.com",
                PasswordHash = HashPassword("admin"),
                Role = Role.Admin,
                IsBlocked = false,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                AvatarUrl = "/Content/Images/avatars/default.png"
            },
            new UserInternal
            {
                Id = 3,
                Username = "user123",
                Email = "user123@example.com",
                PasswordHash = HashPassword("user123"),
                Role = Role.User,
                IsBlocked = false,
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
                AvatarUrl = "/Content/Images/avatars/default.png"
            },
            new UserInternal
            {
                Id = 4,
                Username = "newuser2025",
                Email = "newuser@example.com",
                PasswordHash = HashPassword("newuser"),
                Role = Role.User,
                IsBlocked = false,
                CreatedAt = DateTime.UtcNow,
                AvatarUrl = "/Content/Images/avatars/default.png"
            }
        };

        #endregion

        #region IUserService — реализация

        // ---------- чтение ----------
        public IEnumerable<UserReturn> GetAllUsers()           => _users.Select(ToReturn).ToList();
        public UserReturn GetUserById(int id)                  => ToReturn(_users.FirstOrDefault(u => u.Id == id));
        public UserReturn GetUserByUsername(string username)   => ToReturn(_users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));
        public UserReturn GetUserByEmail(string email)         => ToReturn(_users.FirstOrDefault(u => u.Email   .Equals(email,    StringComparison.OrdinalIgnoreCase)));

        // ---------- аутентификация ----------
        public UserReturn Authenticate(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var hash = HashPassword(password);
            var user = _users.FirstOrDefault(u =>
                         u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                         u.PasswordHash == hash &&
                         !u.IsBlocked);

            if (user != null) user.LastLogin = DateTime.UtcNow;
            return ToReturn(user);
        }

        // ---------- регистрация ----------
        public UserReturn Register(UserRegister dto)
        {
            if (dto == null) return null;

            if (_users.Any(u =>
                    u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase) ||
                    u.Email   .Equals(dto.Email,    StringComparison.OrdinalIgnoreCase)))
                return null;                                       // уже существует

            var user = new UserInternal
            {
                Id           = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
                Username     = dto.Username,
                Email        = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role         = Role.User,
                IsBlocked    = false,
                CreatedAt    = DateTime.UtcNow
            };

            _users.Add(user);
            return ToReturn(user);
        }

        // ---------- сброс пароля ----------
        public string GeneratePasswordResetCode(string email)
        {
            var user = _users.FirstOrDefault(u =>
                         u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                         !u.IsBlocked);

            if (user == null) return null;

            user.ResetPasswordCode           = Guid.NewGuid().ToString("N").Substring(0, 16);
            user.ResetPasswordCodeExpiration = DateTime.UtcNow.AddHours(24);
            return user.ResetPasswordCode;
        }

        public bool ResetPassword(string email, string code, string newPassword)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.ResetPasswordCode == code &&
                u.ResetPasswordCodeExpiration.HasValue &&
                u.ResetPasswordCodeExpiration.Value > DateTime.UtcNow &&
                !u.IsBlocked);

            if (user == null) return false;

            user.PasswordHash                = HashPassword(newPassword);
            user.ResetPasswordCode           = null;
            user.ResetPasswordCodeExpiration = null;
            return true;
        }

        // ---------- администрирование ----------
        public void ToggleUserStatus(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) user.IsBlocked = !user.IsBlocked;
        }

        // ---------- статистика ----------
        public int GetUsersCount()        => _users.Count;
        public int GetActiveUsersCount()  => _users.Count(u => !u.IsBlocked);
        public int GetBlockedUsersCount() => _users.Count(u =>  u.IsBlocked);
        public int GetNewUsersToday()     => _users.Count(u => u.CreatedAt.Date == DateTime.UtcNow.Date);

        #endregion

        #region helpers

        private static UserReturn ToReturn(UserInternal u) =>
            u == null ? null : new UserReturn(
            u.Id,
            u.Username,
            u.Email,
            u.AvatarUrl,
            u.CreatedAt, // ← добавили недостающий параметр
            u.Role,
            u.IsBlocked);
        private static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
            }
        }

        #endregion
    }
}
