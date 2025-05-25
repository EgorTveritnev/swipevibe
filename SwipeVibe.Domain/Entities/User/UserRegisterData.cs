using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SwipeVibe.Domain.Enums;

namespace SwipeVibe.Domain.Entities.User
{
    public class UserRegisterData
    {
        [Required, StringLength(32, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; } = Role.User;
    }
}
