using AutoMapper;
using EmploymentSystemApplication.DTOs.Authentication;
using EmploymentSystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Mapping
{
    public class AuthMapping : Profile
    {
        public AuthMapping() {
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}
