using EmploymentSystemApi.Controllers.Base;
using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.Vacancy;
using EmploymentSystemApplication.Features.Vacancy.Commands.Models;
using EmploymentSystemApplication.Features.Vacancy.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystemApi.Controllers
{
    
    public class VacancyController : BaseController
    {
        public VacancyController(IMediator mediator) : base(mediator) {}

        [Authorize(Roles = "Employer")]
        [HttpPost("AddVacancy")]
        public async Task<IActionResult> CreateVacancyAsync(CreateVacancyDto createVacancyDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(new CreateVacancyCommand(createVacancyDto));

            return StatusCode(command.StatusCode, command);
        }
        [Authorize(Roles = "Employer")]
        [HttpGet("GetVacancy")]
        public async Task<IActionResult> GetVacancyAsync(Guid? id , string? jobTitle)
        {
            var query = await _mediator.Send(new GetVacancyQuery(id, jobTitle));

            return StatusCode(query.StatusCode, query);
        }
        [Authorize(Roles = "Employer")]
        [HttpGet("GetAllVacancies")]
        public async Task<IActionResult> GetAllVacanciesAsync([FromQuery] VacancyFilterParams vacancyFilterParams,[FromQuery] PaginationParams paginationParams)
        {
            var query = await _mediator.Send(new GetAllVacanciesQuery(vacancyFilterParams, paginationParams));

            return StatusCode(query.StatusCode, query);
        }
        [Authorize(Roles = "Employer")]
        [HttpPost("DeActivate/{id}")]
        public async Task<IActionResult> DeActivateVacancyAsync(Guid id)
        {
            var command = await _mediator.Send(new DeActivateVacancyCommand(id));

            return StatusCode(command.StatusCode, command);
        }
        [Authorize(Roles = "Applicant, Employer")]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchVacancyAsync(string searchValue)
        {
            var query = await _mediator.Send(new SearchVacacnyQuery(searchValue));

            return StatusCode(query.StatusCode, query);
        }
        [Authorize(Roles = "Employer")]
        [HttpPut("UpdateVacancy")]
        public async Task<IActionResult> UpdateVacancyAsync(UpdateVacancyDto updateVacancyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(new UpdateVacancyCommand(updateVacancyDto));

            return StatusCode(command.StatusCode, command);
        }
        [Authorize(Roles = "Employer")]
        [HttpDelete("DeleteVacancy/{id}")]
        public async Task<IActionResult> DeleteVacancyAsync(Guid id)
        {
            var command = await _mediator.Send(new DeleteVacancyCommand(id));
            return StatusCode(command.StatusCode, command);
        }
    }
}
