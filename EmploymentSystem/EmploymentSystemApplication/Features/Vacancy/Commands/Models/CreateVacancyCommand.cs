using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;
using MediatR;

namespace EmploymentSystemApplication.Features.Vacancy.Commands.Models
{
    public class CreateVacancyCommand : IRequest<ApiResponse>
    {
        public CreateVacancyDto createVacancyDto { get; set; }

        public CreateVacancyCommand(CreateVacancyDto createVacancyDto)
        {
            this.createVacancyDto = createVacancyDto;
        }
    }
}
