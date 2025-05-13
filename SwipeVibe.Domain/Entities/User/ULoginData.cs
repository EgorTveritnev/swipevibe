using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SwipeVibe.Domain.Entities.User
{
    namespace SwipeVibe.Domain.Entities.User
    {
        public class ULoginData
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, StringLength(100, MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
