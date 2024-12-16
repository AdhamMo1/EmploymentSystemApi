using AutoMapper;
using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;
using EmploymentSystemApplication.ServicesContract;
using EmploymentSystemApplication.UnitOfWorkContract;
using EmploymentSystemDomain.Entities;
using EmploymentSystemDomain.Enums;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace EmploymentSystemApplication.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public VacancyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateVacancyServiceAsync(CreateVacancyDto createVacancyDto)
        {
            try
            {
                var newVacancy = _mapper.Map<Vacancy>(createVacancyDto);
                await _unitOfWork.Repository<Vacancy>().CreateAsync(newVacancy);
                await _unitOfWork.SaveAsync();
                return new ApiResponse
                {
                    Message = "Done Successfully",
                    StatusCode = StatusCodes.Status200OK,
                    IsSuccess = true,
                    Result = _mapper.Map<VacancyResponseDto>(newVacancy)
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false
                };
            }
        }

        public async Task<ApiResponse> DeActivateVacancyServiceAsync(Guid id)
        {
            var vacancy = await _unitOfWork.Repository<Vacancy>().Read(prop => prop.Id == id);
            if (vacancy is null)
                return new ApiResponse { Message = "Vacancy Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            try
            {
                vacancy.Status = VacancyStatus.NotActive.ToString();
                await _unitOfWork.SaveAsync();

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "DeActivate Successfull!",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false
                };
            }
        }

        public async Task<ApiResponse> DeleteVacancyServiceAsync(Guid vacancyId)
        {
            var vacancy = await _unitOfWork.Repository<Vacancy>().Read(prop => prop.Id == vacancyId);
            if (vacancy is null)
                return new ApiResponse { Message = "Vacancy Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            try
            {
                _unitOfWork.Repository<Vacancy>().Delete(vacancy);

                var applications = await _unitOfWork.Repository<ApplicantApplication>().ReadAll(filters: [prop => prop.VacancyId == vacancy.Id], pageSize: vacancy.MaxApplications);
                foreach (var item in applications)
                {
                    _unitOfWork.Repository<ApplicantApplication>().Delete(item);
                    FileService.FileDeleteToServer($"wwwroot/Applicant_Resume/{item.ResumeURL}");
                }

                await _unitOfWork.SaveAsync();

                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Deleted Successfull!",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false
                };
            }
        }

        public async Task<ApiResponse> GetAllVacanciesServiceAsync(VacancyFilterParams vacancyFilterParams, PaginationParams paginationParams)
        {
            var result = await _unitOfWork.Repository<Vacancy>().ReadAll(filters: [vacancyFilterParams.jobTypeOptions is null ? null : x => x.JopType == vacancyFilterParams.jobTypeOptions.ToString(),vacancyFilterParams.status is null ? null :  x => x.Status == vacancyFilterParams.status.ToString()]
                                                                , pageIndex: (int)paginationParams.PageIndex, pageSize: (int)paginationParams.PageSize);
            return new ApiResponse
            {
                Message = "Done Successfully",
                StatusCode = StatusCodes.Status200OK,
                IsSuccess = true,
                Result = _mapper.Map<IEnumerable<VacancyResponseDto>>(result)
            };
        }

        public async Task<ApiResponse> GetVacancyServiceAsync(Guid? id, string? jobTitle)
        {
            if (id is null && jobTitle is null)
                return new ApiResponse { Message = "Enter Vacancy Id or Job Title of this vacancy..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };
                
            var result = new VacancyResponseDto();

            if (id is not null)
                result = _mapper.Map<VacancyResponseDto>(await _unitOfWork.Repository<Vacancy>().Read(x => x.Id == id));

            if (jobTitle is not null)
                result = _mapper.Map<VacancyResponseDto>(await _unitOfWork.Repository<Vacancy>().Read(x => x.JobTitle.ToLower() == jobTitle.ToLower()));

            if(result is null)
                return new ApiResponse { Message = "Vacancy Not Found..!", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            return new ApiResponse
            {
                Result = result,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse> SearchVacancyServiceAsync(string searchValue)
        {
            try
            {
                var vacancies = await _unitOfWork.Repository<Vacancy>().ReadAll(filters: [prop => prop.JobTitle.ToLower().Contains(searchValue.ToLower())]);

                if(vacancies == null)
                    return new ApiResponse { Message = "Vacancy Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

                return new ApiResponse
                {
                    Result = _mapper.Map<IEnumerable<VacancyResponseDto>>(vacancies),
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false
                };
            }
        }

        public async Task<ApiResponse> UpdateVacancyServiceAsync(UpdateVacancyDto updateVacancyDto)
        {
            var vacancy = await _unitOfWork.Repository<Vacancy>().Read(prop => prop.Id == updateVacancyDto.Id);
            if(vacancy is null)
                return new ApiResponse { Message = "Vacancy Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            try
            {
                vacancy.JobTitle = updateVacancyDto.JobTitle ?? vacancy.JobTitle;
                vacancy.JobDescription = updateVacancyDto.JobDescription ?? vacancy.JobDescription;
                vacancy.Location = updateVacancyDto.Location ?? vacancy.Location;
                vacancy.ExpiryDate = updateVacancyDto.ExpiryDate ?? vacancy.ExpiryDate;
                vacancy.JopType = updateVacancyDto.JopType.ToString() ?? vacancy.JopType;
                vacancy.Status = updateVacancyDto.Status.ToString() ?? vacancy.Status;
                vacancy.MaxApplications = updateVacancyDto.MaxApplications ?? vacancy.MaxApplications;
                vacancy.Salary = updateVacancyDto.Salary ?? vacancy.Salary;

                await _unitOfWork.SaveAsync();

                return new ApiResponse
                {
                    Result = _mapper.Map<VacancyResponseDto>(vacancy),
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status400BadRequest,
                    IsSuccess = false
                };
            }
        }
    }
}
