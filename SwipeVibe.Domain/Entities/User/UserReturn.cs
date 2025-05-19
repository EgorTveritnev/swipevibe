using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.Domain.Entities.User
{
    public class UserReturn
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActive => !IsBlocked;

        public UserReturn() { }
    }
}