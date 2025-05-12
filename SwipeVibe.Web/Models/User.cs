using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwipeVibe.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
        public string ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiration { get; set; }
    }
}