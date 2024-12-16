using EmploymentSystemApplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Vacancy.Queries.Models
{
    public class GetVacancyQuery : IRequest<ApiResponse>
    {
        public Guid? Id { get; set; }
        public string? JobTitle { get; set; }

        public GetVacancyQuery(Guid? id, string? jobTitle)
        {
            Id = id;
            JobTitle = jobTitle;
        }
    }
}
