using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Vacancy.Queries.Models
{
    public class GetAllVacanciesQuery : IRequest<ApiResponse>
    {
        public VacancyFilterParams VacancyFilterParams { get; set; }
        public PaginationParams PaginationParams { get; set; }
        public GetAllVacanciesQuery(VacancyFilterParams vacancyFilterParams, PaginationParams paginationParams)
        {
            VacancyFilterParams = vacancyFilterParams;
            PaginationParams = paginationParams;
        }

    }
}
