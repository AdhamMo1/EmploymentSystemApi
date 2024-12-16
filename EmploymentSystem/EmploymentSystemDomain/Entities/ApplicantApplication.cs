using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentSystemDomain.Entities
{
    public class ApplicantApplication
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ResumeURL {  get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ForeignKey("Applicant")]
        public string ApplicantId { get; set; }
        public ApplicationUser Applicant { get; set; }
        [ForeignKey("Vacancy")]
        public Guid VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
