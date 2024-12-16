using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.Features.ApplicantApplication.Queries.Models;
using EmploymentSystemApplication.ServicesContract;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Queries.Handlers
{
    public class ApplicantApplicationHandler : IRequestHandler<GetAllApplicationQuery, ApiResponse>,
                                               IRequestHandler<GetApplicationQuery, ApiResponse>
    {
        private readonly IApplicantApplicationService _applicantApplicationService;

        public ApplicantApplicationHandler(IApplicantApplicationService applicantApplication)
        {
            _applicantApplicationService = applicantApplication;
        }

        public async Task<ApiResponse> Handle(GetAllApplicationQuery request, CancellationToken cancellationToken)
        {
            return await _applicantApplicationService.GetAllApplicationServiceAsync(request.ApplicationFilterParams, request.PaginationParams);
        }

        public async Task<ApiResponse> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            return await _applicantApplicationService.GetApplicationServiceAsync(request.Id);
        }
    }
}
