﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmploymentSystemApplication.DTOs.ApplicantApplication
{
    public class UpdateApplicationDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile Resume { get; set; }
        public Guid? VacancyId { get; set; }
    }
}
