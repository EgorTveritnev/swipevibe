using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwipeVibe.Domain.Entities.User
{
    public enum Role { User = 0, Admin = 1 }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Avatar URL")]
        [StringLength(200)]
        public string AvatarUrl { get; set; }

        [Display(Name = "Reset Code Expiry")]
        public DateTime? ResetPasswordCodeExpiration { get; set; }

        [Display(Name = "Reset Code")]
        public string ResetPasswordCode { get; set; }

        [Display(Name = "User Role")]
        public Role Role { get; set; } = Role.User;

        [Display(Name = "Is Blocked")]
        public bool IsBlocked { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }
    }
}

