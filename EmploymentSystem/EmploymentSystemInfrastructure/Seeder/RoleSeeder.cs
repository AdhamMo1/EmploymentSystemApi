using EmploymentSystemDomain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemInfrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> _roleManager)
        {
            if(await _roleManager.Roles.CountAsync() == 0)
            {
                await _roleManager.CreateAsync(new IdentityRole(RolesOptions.Employer.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(RolesOptions.Applicant.ToString()));
            }
        }
    }
}
