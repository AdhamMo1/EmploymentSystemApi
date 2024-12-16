using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmploymentSystemApplication.DTOs.ApplicantApplication
{
    public class CreateApplicationDto
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile Resume { get; set; }
        public Guid VacancyId { get; set; }
    }
}
