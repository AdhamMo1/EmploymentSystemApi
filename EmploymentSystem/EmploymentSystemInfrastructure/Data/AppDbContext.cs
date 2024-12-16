using System.Reflection;
using EmploymentSystemDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystemInfrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
    
    public DbSet<Vacancy> vacancies { get; set; }
    public DbSet<ApplicantApplication> ApplicantsApplications { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // apply configuration
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // change identity tables name and schema.
        builder.Entity<ApplicationUser>().ToTable("Users", "security");
        builder.Entity<IdentityRole>().ToTable("Roles", "security");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
    }
}