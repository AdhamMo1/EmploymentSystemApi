using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;

namespace EmploymentSystemApplication.ServicesContract
{
    public interface IApplicantApplicationService
    {
        Task<ApiResponse> CreateApplicationServiceAsync(CreateApplicationDto createApplicationDto);
        Task<ApiResponse> GetApplicationServiceAsync(Guid? id);
        Task<ApiResponse> GetAllApplicationServiceAsync(ApplicationFilterParams ApplicationFilterParams, PaginationParams paginationParams);
        Task<ApiResponse> UpdateApplicationServiceAsync(UpdateApplicationDto updateApplicationDto);
        Task<ApiResponse> DeleteApplicationServiceAsync(Guid ApplicationId);
    }
}
