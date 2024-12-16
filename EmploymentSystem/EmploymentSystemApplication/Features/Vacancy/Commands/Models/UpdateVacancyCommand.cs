using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Vacancy.Commands.Models
{
    public class UpdateVacancyCommand : IRequest<ApiResponse>
    {
        public UpdateVacancyDto UpdateVacancyDto { get; set; }

        public UpdateVacancyCommand(UpdateVacancyDto updateVacancyDto)
        {
            UpdateVacancyDto = updateVacancyDto;
        }
    }
}
