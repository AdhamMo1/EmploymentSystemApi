using EmploymentSystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.DTOs.Vacancy
{
    public class VacancyFilterParams
    {
        [Range(0, 3, ErrorMessage = "Error in Job Type, 0 - Full_Time , 1 - Part_Time , 2 - Contract , 3 - Internship")]
        public JobTypeOptions? jobTypeOptions { get; set; }
        [Range(0, 1, ErrorMessage = "Error in Vacancy Status, 0 - Active , 1 - NotActive")]
        public VacancyStatus? status { get; set; }
    }
}
