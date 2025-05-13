using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.BusinessLogic.Interfaces;
using SwipeVibe.Domain.Entities.User;

namespace SwipeVibe.BusinessLogic
{
    internal class UserRepositoryBL : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _seq = 1;

        public IEnumerable<User> All() => _users;
        public User ById(int id) => _users.SingleOrDefault(u => u.Id == id);
        public User ByEmail(string email) => _users.SingleOrDefault(u => u.Email == email);

        public void Add(User user) => _users.Add(user);

        public void Update(User user)
        {

            var idx = _users.FindIndex(u => u.Id == user.Id);
            if (idx >= 0) _users[idx] = user;
        }

        public int NextId() => _seq++;
    }
}
