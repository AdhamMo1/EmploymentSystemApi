using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Commands.Models
{
    public class UpdateApplicationCommand : IRequest<ApiResponse>
    {
        public UpdateApplicationDto UpdateApplicationDto { get; set; }
        public UpdateApplicationCommand(UpdateApplicationDto updateApplicationDto)
        {
            UpdateApplicationDto = updateApplicationDto;
        }

    }
}
