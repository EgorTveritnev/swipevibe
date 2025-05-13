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
        public string AvatarUrl { get; }
        public DateTime RegisteredDate { get; }
        public Role Role { get; }
        public bool IsBlocked { get; }
        public bool IsActive => !IsBlocked;
        public UserReturn(int id, string username, string email, string avatarUrl, DateTime registeredDate, Role role, bool isBlocked)
        {
            Id = id;
            Username = username;
            Email = email;
            AvatarUrl = avatarUrl;
            Role = role;
            IsBlocked = isBlocked;
        }
    }
}