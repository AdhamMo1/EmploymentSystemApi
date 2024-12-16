using EmploymentSystemDomain.Entities;
using EmploymentSystemDomain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemInfrastructure.Configuration
{
    public class VacancyConfig : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(prop => prop.JobTitle)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(prop => prop.JobDescription)
                .HasMaxLength(1500)
                .IsRequired();

            builder.Property(prop => prop.JopType)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(prop => prop.Location)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.ExpiryDate)
                .IsRequired();

            builder.Property(prop => prop.Salary)
                .IsRequired();

            builder.Property(prop => prop.PostedDate)
                .IsRequired();

            builder.Property(prop => prop.Status)
                .HasMaxLength(25)
                .HasDefaultValue(VacancyStatus.Active.ToString())
                .IsRequired();

            builder.Property(prop => prop.MaxApplications)
                .IsRequired();
        }
    }
}
