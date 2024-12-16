using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Queries.Models
{
    public class GetAllApplicationQuery : IRequest<ApiResponse>
    {
        public ApplicationFilterParams ApplicationFilterParams { get; set; }
        public PaginationParams PaginationParams { get; set; }
        public GetAllApplicationQuery(ApplicationFilterParams applicationFilterParams, PaginationParams paginationParams)
        {
            ApplicationFilterParams = applicationFilterParams;
            PaginationParams = paginationParams;
        }
    }
}
