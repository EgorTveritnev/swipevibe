using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SwipeVibe.Domain.Entities.User
{
    public class UserUpdate
    {
        [StringLength(32, MinimumLength = 3)]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
