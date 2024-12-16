using EmploymentSystemApi.Controllers.Base;
using EmploymentSystemApplication.DTOs;
using EmploymentSystemApplication.DTOs.ApplicantApplication;
using EmploymentSystemApplication.Features.ApplicantApplication.Commands.Models;
using EmploymentSystemApplication.Features.ApplicantApplication.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystemApi.Controllers
{
    public class ApplicationController : BaseController
    {
        public ApplicationController(IMediator mediator) : base(mediator) { }

        [Authorize(Roles = "Applicant")]
        [HttpPost("AddApplication")]
        public async Task<IActionResult> CreateApplicationAsync(CreateApplicationDto createApplicationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(new CreateApplicationCommand(createApplicationDto));

            return StatusCode(command.StatusCode, command);
        }
        [Authorize(Roles = "Applicant, Employer")]
        [HttpGet("GetApplication")]
        public async Task<IActionResult> GetApplicationAsync(Guid? id)
        {
            var query = await _mediator.Send(new GetApplicationQuery(id));

            return StatusCode(query.StatusCode, query);
        }
        [Authorize(Roles = "Employer")]
        [HttpGet("GetAllApplications")]
        public async Task<IActionResult> GetAllApplicationsAsync([FromQuery] ApplicationFilterParams applicationFilterParams, [FromQuery] PaginationParams paginationParams)
        {
            var query = await _mediator.Send(new GetAllApplicationQuery(applicationFilterParams, paginationParams));

            return StatusCode(query.StatusCode, query);
        }
        [Authorize(Roles = "Applicant")]
        [HttpPut("UpdateApplication")]
        public async Task<IActionResult> UpdateApplicationAsync(UpdateApplicationDto updateApplicationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(new UpdateApplicationCommand(updateApplicationDto));

            return StatusCode(command.StatusCode, command);
        }
        [Authorize(Roles = "Applicant, Employer")]
        [HttpDelete("DeleteApplication/{id}")]
        public async Task<IActionResult> DeleteVacancyAsync(Guid id)
        {
            var command = await _mediator.Send(new DeleteApplicationCommand(id));
            return StatusCode(command.StatusCode, command);
        }
    }
}
