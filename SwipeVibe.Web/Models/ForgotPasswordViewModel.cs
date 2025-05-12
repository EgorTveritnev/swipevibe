using System;
using System.ComponentModel.DataAnnotations;

namespace SwipeVibe.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите корректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}