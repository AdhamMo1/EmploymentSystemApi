using AutoMapper;
using EmploymentSystemApplication.DTOs.Vacancy;
using EmploymentSystemDomain.Entities;
using EmploymentSystemDomain.Enums;

namespace EmploymentSystemApplication.Mapping
{
    public class VacancyMapping : Profile
    {
        public VacancyMapping()
        {
            CreateMap<CreateVacancyDto, Vacancy>().ForMember(prop => prop.JopType, opt => opt.MapFrom(z => z.JopType.ToString()));

            CreateMap<Vacancy,VacancyResponseDto>();

            CreateMap<UpdateVacancyDto, Vacancy>().ForMember(prop => prop.JopType, opt => opt.MapFrom(z => z.JopType.ToString()))
                .ForMember(prop => prop.Status, opt => opt.MapFrom(z => z.Status.ToString()));
        }
    }
}
