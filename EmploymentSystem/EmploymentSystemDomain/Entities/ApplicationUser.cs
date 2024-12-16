using EmploymentSystemDomain.Entities.SecurityModels;
using Microsoft.AspNetCore.Identity;

namespace EmploymentSystemDomain.Entities;

public class ApplicationUser : IdentityUser
{
    public List<RefreshToken>? RefreshTokens { get; set; }
    public List<ApplicantApplication>? applicantApplications { get; set; }
}