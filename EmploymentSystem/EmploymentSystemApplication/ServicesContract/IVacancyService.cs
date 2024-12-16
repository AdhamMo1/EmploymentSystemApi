using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;

namespace EmploymentSystemApplication.ServicesContract
{
    public interface IVacancyService
    {
        Task<ApiResponse> CreateVacancyServiceAsync(CreateVacancyDto createVacancyDto);
        Task<ApiResponse> GetVacancyServiceAsync(Guid? id , string? jobTitle);
        Task<ApiResponse> GetAllVacanciesServiceAsync(VacancyFilterParams vacancyFilterParams,PaginationParams paginationParams);
        Task<ApiResponse> DeActivateVacancyServiceAsync(Guid id);
        Task<ApiResponse> SearchVacancyServiceAsync(string searchValue);
        Task<ApiResponse> UpdateVacancyServiceAsync(UpdateVacancyDto updateVacancyDto);
        Task<ApiResponse> DeleteVacancyServiceAsync(Guid vacancyId);
    }
}
