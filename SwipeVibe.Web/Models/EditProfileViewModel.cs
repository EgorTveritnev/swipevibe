using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SwipeVibe.Web.Models
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя пользователя должно содержать от 3 до 30 символов")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Загрузить аватар")]
        public HttpPostedFileBase Avatar { get; set; }

        public string CurrentAvatarUrl { get; set; }
    }
}