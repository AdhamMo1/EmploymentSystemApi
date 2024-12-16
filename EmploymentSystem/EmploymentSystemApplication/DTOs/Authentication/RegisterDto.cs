using EmploymentSystemDomain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystemApplication.DTOs.Authentication
{
    public class RegisterDto
    {
        [MaxLength(25)]
        public string UserName {  get; set; }
        [MaxLength(50),EmailAddress]
        public string Email {  get; set; }
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
            ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Range(0,1,ErrorMessage ="Range between 0 : Employer and 1 : Applicant"),Required]
        public RolesOptions Role { get; set; }
    }
}
