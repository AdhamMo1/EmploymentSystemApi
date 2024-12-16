using EmploymentSystemApplication.DTOs;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Commands.Models
{
    public class DeleteApplicationCommand : IRequest<ApiResponse>
    {
        public Guid id { get; set; }

        public DeleteApplicationCommand(Guid Id)
        {
            id = Id;
        }
    }
}
