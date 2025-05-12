using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SwipeVibe.Web.Models
{
    public class UserService
    {
        // Демо-хранилище пользователей (в реальном проекте здесь будет работа с БД)
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "demo",
                Email = "demo@swipevibe.com",
                PasswordHash = HashPassword("password"),
                AvatarUrl = "/Content/Images/avatars/default.png",
                RegisteredDate = DateTime.Now.AddDays(-30),
                IsActive = true
            }
        };

        // Аутентификация пользователя
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var passwordHash = HashPassword(password);
            var user = _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower() && u.PasswordHash == passwordHash && u.IsActive);

            return user;
        }

        // Регистрация нового пользователя
        public User Register(RegisterViewModel model)
        {
            if (model == null)
                return null;

            // Проверяем, что пользователь с таким email или именем не существует
            if (_users.Any(u => u.Email.ToLower() == model.Email.ToLower() || u.Username.ToLower() == model.Username.ToLower()))
                return null;

            var user = new User
            {
                Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = HashPassword(model.Password),
                AvatarUrl = "/Content/Images/avatars/default.png",
                RegisteredDate = DateTime.Now,
                IsActive = true
            };

            _users.Add(user);
            return user;
        }

        // Генерация кода для сброса пароля
        public string GeneratePasswordResetCode(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower() && u.IsActive);
            if (user == null)
                return null;

            // Генерируем случайный код
            string resetCode = Guid.NewGuid().ToString("N").Substring(0, 16);
            user.ResetPasswordCode = resetCode;
            user.ResetPasswordCodeExpiration = DateTime.Now.AddHours(24); // Код действителен 24 часа

            return resetCode;
        }

        // Сброс пароля
        public bool ResetPassword(string email, string code, string newPassword)
        {
            var user = _users.FirstOrDefault(u => 
                u.Email.ToLower() == email.ToLower() &&
                u.ResetPasswordCode == code &&
                u.ResetPasswordCodeExpiration.HasValue &&
                u.ResetPasswordCodeExpiration.Value > DateTime.Now &&
                u.IsActive);

            if (user == null)
                return false;

            // Обновляем пароль
            user.PasswordHash = HashPassword(newPassword);
            user.ResetPasswordCode = null;
            user.ResetPasswordCodeExpiration = null;

            return true;
        }

        // Хеширование пароля
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}