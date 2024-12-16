using System.ComponentModel.DataAnnotations;

namespace EmploymentSystemApplication.DTOs.ApplicantApplication
{
    public class ApplicationResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ResumeURL { get; set; }
        public string ApplicantId { get; set; }
        public Guid VacancyId { get; set; }
    }
}
