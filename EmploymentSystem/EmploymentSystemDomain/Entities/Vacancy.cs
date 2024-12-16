using EmploymentSystemDomain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmploymentSystemDomain.Entities
{
    public class Vacancy
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JopType { get; set; }
        public double Salary { get; set; }
        public string Location { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
        public string Status { get; set; }
        public List<ApplicantApplication>? ApplicantsApplications {  get; set; }
        public Vacancy()
        {
            PostedDate = DateTime.Now;
            Status = DateTime.Now >= ExpiryDate ? VacancyStatus.NotActive.ToString() : VacancyStatus.Active.ToString();
        }
    }
}
