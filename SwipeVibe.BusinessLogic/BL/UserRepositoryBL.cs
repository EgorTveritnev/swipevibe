using System;
using System.Collections.Generic;
using System.Linq;
using SwipeVibe.Domain.Entities.User;
using SwipeVibe.BusinessLogic.Interfaces;

namespace SwipeVibe.BusinessLogic.BL
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "demo",
                Email = "demo@swipevibe.com",
                Password = "password",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                Role = Role.User,
                IsBlocked = false
            },
             new User
            {
                Id = 2,
                Username = "admin",
                Email = "admin@swipevibe.com",
                Password = "admin",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                Role = Role.Admin,
                IsBlocked = false
            }
        };

        public IEnumerable<User> All() => _users;

        public User ById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public User ByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public User ByUsername(string username) =>
            _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        public void Add(User user)
        {
            user.Id = NextId();
            user.CreatedAt = DateTime.UtcNow;
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.Username = user.Username;
                existing.Email = user.Email;
                existing.Password = user.Password;
                existing.Role = user.Role;
                existing.IsBlocked = user.IsBlocked;
            }
        }

        public int NextId() => _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
    }
}
