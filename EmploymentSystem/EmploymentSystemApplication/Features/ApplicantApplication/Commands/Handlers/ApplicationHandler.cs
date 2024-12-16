using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.Features.ApplicantApplication.Commands.Models;
using EmploymentSystemApplication.ServicesContract;
using MediatR;

namespace EmploymentSystemApplication.Features.ApplicantApplication.Commands.Handlers
{
    internal class ApplicationHandler : IRequestHandler<CreateApplicationCommand, ApiResponse>,
                                        IRequestHandler<DeleteApplicationCommand, ApiResponse>,
                                        IRequestHandler<UpdateApplicationCommand, ApiResponse>
    {
        private readonly IApplicantApplicationService _applicationService;

        public ApplicationHandler(IApplicantApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public async Task<ApiResponse> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _applicationService.CreateApplicationServiceAsync(request.createApplicationDto);
        }

        public async Task<ApiResponse> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _applicationService.DeleteApplicationServiceAsync(request.id);
        }

        public async Task<ApiResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            return await  _applicationService.UpdateApplicationServiceAsync(request.UpdateApplicationDto);
        }
    }
}
