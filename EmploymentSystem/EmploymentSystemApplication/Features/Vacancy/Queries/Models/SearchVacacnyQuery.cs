using EmploymentSystemApplication.DTOs;
using MediatR;

namespace EmploymentSystemApplication.Features.Vacancy.Queries.Models
{
    public class SearchVacacnyQuery : IRequest<ApiResponse>
    {
        public string searchValue { get; set; }

        public SearchVacacnyQuery(string searchValue)
        {
            this.searchValue = searchValue;
        }
    }
}
