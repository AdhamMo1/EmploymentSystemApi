using EmploymentSystemDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemInfrastructure.Configuration
{
    public class ApplicantApplicationConfig : IEntityTypeConfiguration<ApplicantApplication>
    {
        public void Configure(EntityTypeBuilder<ApplicantApplication> builder)
        {
            builder.Property(prop => prop.Id).IsRequired();

            builder.Property(prop => prop.FullName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(prop => prop.EmailAddress)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(prop => prop.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(prop => prop.ResumeURL)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
