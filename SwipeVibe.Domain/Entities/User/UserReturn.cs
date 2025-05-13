using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SwipeVibe.Domain.Entities.User
{
    public class UserReturn
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }
        public Role Role { get; }
        public bool IsBlocked { get; }

        public UserReturn(int id, string username, string email, Role role, bool isBlocked)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = role;
            IsBlocked = isBlocked;
        }
    }
}