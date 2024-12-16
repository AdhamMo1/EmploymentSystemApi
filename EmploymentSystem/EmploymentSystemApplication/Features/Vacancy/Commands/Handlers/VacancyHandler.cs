using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.Features.Vacancy.Commands.Models;
using EmploymentSystemApplication.ServicesContract;
using MediatR;

namespace EmploymentSystemApplication.Features.Vacancy.Commands.Handlers
{
    public class VacancyHandler : IRequestHandler<CreateVacancyCommand, ApiResponse>,
                                  IRequestHandler<UpdateVacancyCommand, ApiResponse>,
                                  IRequestHandler<DeleteVacancyCommand, ApiResponse>,
                                  IRequestHandler<DeActivateVacancyCommand, ApiResponse>
    {
        private readonly IVacancyService _vacancyService;

        public VacancyHandler(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public async Task<ApiResponse> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyService.CreateVacancyServiceAsync(request.createVacancyDto);
        }

        public async Task<ApiResponse> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyService.UpdateVacancyServiceAsync(request.UpdateVacancyDto);
        }

        public async Task<ApiResponse> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyService.DeleteVacancyServiceAsync(request.VacancyId);
        }

        public async Task<ApiResponse> Handle(DeActivateVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _vacancyService.DeActivateVacancyServiceAsync(request.id);
        }
    }
}
