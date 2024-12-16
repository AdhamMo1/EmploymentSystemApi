using EmploymentSystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.DTOs.Vacancy
{
    public class CreateVacancyDto
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public JobTypeOptions JopType { get; set; }
        public double Salary { get; set; }
        public string Location { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxApplications { get; set; }
    }
}
