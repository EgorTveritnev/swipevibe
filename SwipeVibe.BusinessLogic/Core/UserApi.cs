using System;
using System.Collections.Generic;
using System.Linq;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.BusinessLogic.DBModel;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.BusinessLogic.Core
{
    public class UserApi
    {
        protected UserReturn ByIdAction(int id)
        {
            using (var db = new UserContext())
            {
                var u = db.Users.FirstOrDefault(x => x.Id == id);
                if (u == null) return null;

                return new UserReturn
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    AvatarUrl = u.AvatarUrl,
                    RegisteredDate = u.CreatedAt,
                    Role = u.Role.ToString(),
                    IsBlocked = u.IsBlocked
                };
            }
        }

        protected void UpdateAction(int id, UserUpdate upd)
        {
            using (var db = new UserContext())
            {
                var u = db.Users.FirstOrDefault(x => x.Id == id);
                if (u == null) return;

                if (!string.IsNullOrWhiteSpace(upd.Username))
                    u.Username = upd.Username;

                if (!string.IsNullOrWhiteSpace(upd.Email))
                    u.Email = upd.Email;

                if (!string.IsNullOrWhiteSpace(upd.NewPassword))
                    u.Password = upd.NewPassword;

                db.SaveChanges();
            }
        }

        protected UserLoginResult LoginAction(UserLoginData creds)
        {
            using (var db = new UserContext())
            {
                var u = db.Users
                    .FirstOrDefault(x => x.Email == creds.Email && x.Password == creds.Password);

                if (u == null)
                    return new UserLoginResult
                    {
                        Status = false,
                        StatusMsg = "Invalid email or password.",
                        UserInfo = null
                    };

                u.LastLogin = System.DateTime.UtcNow;
                db.SaveChanges();

                return new UserLoginResult
                {
                    Status = true,
                    StatusMsg = "Login successful.",
                    UserInfo = new UserReturn
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        AvatarUrl = u.AvatarUrl,
                        RegisteredDate = u.CreatedAt,
                        Role = u.Role.ToString(),
                        IsBlocked = u.IsBlocked
                    }
                };
            }
        }

        protected UserRegisterResult RegisterAction(UserRegisterData dto)
        {
            using (var db = new UserContext())
            {
                if (db.Users.Any(x => x.Email == dto.Email))
                    return new UserRegisterResult
                    {
                        Status = false,
                        StatusMsg = "User already exists."
                    };

                var u = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password,
                    CreatedAt = System.DateTime.UtcNow,
                    Role = Role.User
                };

                db.Users.Add(u);
                db.SaveChanges();

                return new UserRegisterResult
                {
                    Status = true,
                    StatusMsg = "Registration successful.",
                    User = u
                };
            }
        }
        public void UpdateAvatar(int userId, string avatarUrl)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null) throw new Exception("User not found");
                user.AvatarUrl = avatarUrl;
                db.SaveChanges();
            }
        }
        protected void LogoutAction(int userId)
        {
        }
    }
}