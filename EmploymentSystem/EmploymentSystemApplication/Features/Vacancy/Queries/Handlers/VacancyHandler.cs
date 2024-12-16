using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.Features.Vacancy.Queries.Models;
using EmploymentSystemApplication.ServicesContract;
using MediatR;

namespace EmploymentSystemApplication.Features.Vacancy.Queries.Handlers
{
    public class VacancyHandler : IRequestHandler<GetVacancyQuery, ApiResponse>,
                                  IRequestHandler<GetAllVacanciesQuery, ApiResponse>,
                                  IRequestHandler<SearchVacacnyQuery, ApiResponse>
    {
        private readonly IVacancyService _vacancyService;

        public VacancyHandler(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public async Task<ApiResponse> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyService.GetVacancyServiceAsync(request.Id, request.JobTitle);
        }

        public async Task<ApiResponse> Handle(GetAllVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyService.GetAllVacanciesServiceAsync(request.VacancyFilterParams, request.PaginationParams);
        }

        public async Task<ApiResponse> Handle(SearchVacacnyQuery request, CancellationToken cancellationToken)
        {
            return await _vacancyService.SearchVacancyServiceAsync(request.searchValue);
        }
    }
}
