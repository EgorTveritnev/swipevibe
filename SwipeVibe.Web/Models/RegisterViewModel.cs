using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SwipeVibe.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя пользователя должно содержать от 3 до 30 символов")]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Я согласен с условиями использования")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Вы должны согласиться с условиями использования")]
        public bool AcceptTerms { get; set; }
    }
}