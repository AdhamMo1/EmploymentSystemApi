using AutoMapper;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using EmploymentSystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<CreateApplicationDto,ApplicantApplication>();
            CreateMap<ApplicantApplication, ApplicationResponseDto>();
        }
    }
}
