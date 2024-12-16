using EmploymentSystemApplication.DTOs;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Queries.Models
{
    public class GetApplicationQuery : IRequest<ApiResponse>
    {
        public Guid? Id { get; set; }

        public GetApplicationQuery(Guid? id)
        {
            Id = id;
        }
    }
}
