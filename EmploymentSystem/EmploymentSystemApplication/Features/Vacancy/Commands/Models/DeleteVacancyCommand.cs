using EmploymentSystemApplication.DTOs;
using MediatR;

namespace EmploymentSystemApplication.Features.Vacancy.Commands.Models
{
    public class DeleteVacancyCommand : IRequest<ApiResponse>
    {
        public Guid VacancyId { get; set; }
        public DeleteVacancyCommand(Guid vacancyId)
        {
            VacancyId = vacancyId;
        }

    }
}
