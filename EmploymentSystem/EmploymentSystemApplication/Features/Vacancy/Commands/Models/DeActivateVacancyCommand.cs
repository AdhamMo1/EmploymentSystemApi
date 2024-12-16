using EmploymentSystemApplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemApplication.Features.Vacancy.Commands.Models
{
    public class DeActivateVacancyCommand : IRequest<ApiResponse>
    {
        public Guid id { get; set; }

        public DeActivateVacancyCommand(Guid id)
        {
            this.id = id;
        }
    }
}
