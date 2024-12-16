using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Commands.Models
{
    public class CreateApplicationCommand : IRequest<ApiResponse>
    {
        public CreateApplicationDto createApplicationDto { get; set; }

        public CreateApplicationCommand(CreateApplicationDto createApplicationDto)
        {
            this.createApplicationDto = createApplicationDto;
        }
    }
}
