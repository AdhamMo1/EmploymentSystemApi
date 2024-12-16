using AutoMapper;
using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using EmploymentSystemApplication.DTOs.Vacancy;
using EmploymentSystemApplication.ServicesContract;
using EmploymentSystemApplication.UnitOfWorkContract;
using EmploymentSystemDomain.Entities;
using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EmploymentSystemApplication.Services
{
    public class ApplicantApplicationService : IApplicantApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicantApplicationService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        private string GetCurrentUserName()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            return userName;
        }
        public async Task<ApiResponse> CreateApplicationServiceAsync(CreateApplicationDto createApplicationDto)
        {
            try
            {
                var Applicant = await _userManager.FindByNameAsync(GetCurrentUserName());
                if (Applicant == null)
                    return new ApiResponse { Message = "Applicant Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

                var latestApplications = await _unitOfWork.Repository<ApplicantApplication>().ReadAll([prop => prop.ApplicantId == Applicant.Id]);
                var latestApplication = latestApplications.OrderByDescending(prop => prop.CreatedOn).FirstOrDefault();

                if (latestApplication != null) {
                    if ((DateTime.Now - latestApplication.CreatedOn).TotalDays < 1)
                    {
                        return new ApiResponse { Message = "Not allowed to apply for more than one vacancy per day (24 hours) try Tommorw!..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };
                    }
                }

                var vacancy = await _unitOfWork.Repository<Vacancy>().Read(filter: prop => prop.Id == createApplicationDto.VacancyId);
                if(vacancy == null)
                    return new ApiResponse { Message = "Vacancy Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };


                var countOfApplication = await _unitOfWork.Repository<ApplicantApplication>().ReadAll(filters: [prop => prop.VacancyId == createApplicationDto.VacancyId], pageSize: vacancy.MaxApplications);

                if(countOfApplication.Count() == vacancy.MaxApplications)
                    return new ApiResponse { Message = "Vacancy have max number of application..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

                var newApplication = _mapper.Map<ApplicantApplication>(createApplicationDto);
                newApplication.ResumeURL = FileService.FileSaveToServer(createApplicationDto.Resume, "wwwroot/Applicant_Resume/");
                newApplication.ApplicantId = Applicant.Id;

                await _unitOfWork.Repository<ApplicantApplication>().CreateAsync(newApplication);
                await _unitOfWork.SaveAsync();
                return new ApiResponse
                {
                    Message = "Done Successfully",
                    StatusCode = StatusCodes.Status200OK,
                    IsSuccess = true,
                    Result = _mapper.Map<ApplicationResponseDto>(newApplication)
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

        public async Task<ApiResponse> DeleteApplicationServiceAsync(Guid ApplicationId)
        {
            var application = await _unitOfWork.Repository<ApplicantApplication>().Read(prop => prop.Id == ApplicationId);
            if (application is null)
                return new ApiResponse { Message = "Application Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            try
            {
                _unitOfWork.Repository<ApplicantApplication>().Delete(application);
                FileService.FileDeleteToServer($"wwwroot/Applicant_Resume/{application.ResumeURL}");

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

        public async Task<ApiResponse> GetAllApplicationServiceAsync(ApplicationFilterParams applicationFilterParams, PaginationParams paginationParams)
        {
            var result = await _unitOfWork.Repository<ApplicantApplication>().ReadAll(filters: [applicationFilterParams.VacancyId is null ? null : prop => prop.VacancyId == applicationFilterParams.VacancyId ]
                                                                , pageIndex: (int)paginationParams.PageIndex, pageSize: (int)paginationParams.PageSize);
            return new ApiResponse
            {
                Message = "Done Successfully",
                StatusCode = StatusCodes.Status200OK,
                IsSuccess = true,
                Result = _mapper.Map<IEnumerable<ApplicationResponseDto>>(result)
            };
        }

        public async Task<ApiResponse> GetApplicationServiceAsync(Guid? id)
        {
            if (id is null)
                return new ApiResponse { Message = "Enter Application Id ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            var result = new ApplicationResponseDto();

            if (id is not null)
                result = _mapper.Map<ApplicationResponseDto>(await _unitOfWork.Repository<ApplicantApplication>().Read(x => x.Id == id));

            if (result is null)
                return new ApiResponse { Message = "Application Not Found..!", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            return new ApiResponse
            {
                Result = result,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse> UpdateApplicationServiceAsync(UpdateApplicationDto updateApplicationDto)
        {
            var application = await _unitOfWork.Repository<ApplicantApplication>().Read(prop => prop.Id == updateApplicationDto.Id);
            if (application is null)
                return new ApiResponse { Message = "Application Not Found ..", StatusCode = StatusCodes.Status400BadRequest, IsSuccess = false };

            try
            {
                application.FullName = updateApplicationDto.FullName ?? application.FullName;
                application.EmailAddress = updateApplicationDto.EmailAddress ?? application.EmailAddress;
                application.PhoneNumber = updateApplicationDto.PhoneNumber ?? application.PhoneNumber;
                application.Address = updateApplicationDto.Address ?? application.Address;
                application.VacancyId = updateApplicationDto.VacancyId ?? application.VacancyId;

                if(updateApplicationDto.Resume is not null)
                {
                    FileService.FileDeleteToServer($"wwwroot/Applicant_Resume/{application.ResumeURL}");
                    application.ResumeURL = FileService.FileSaveToServer(updateApplicationDto.Resume, "wwwroot/Applicant_Resume/");
                }

                await _unitOfWork.SaveAsync();

                return new ApiResponse
                {
                    Result = _mapper.Map<ApplicationResponseDto>(application),
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
