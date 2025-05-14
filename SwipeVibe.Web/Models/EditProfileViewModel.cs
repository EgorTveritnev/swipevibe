using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SwipeVibe.Web.Models
{
    public class EditProfileViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Загрузить аватар")]
        public HttpPostedFileBase Avatar { get; set; }

        public string CurrentAvatarUrl { get; set; }
    }
}