using CqrsMediatr.Application.Members.Commands;
using CqrsMediatr.Application.Members.Queries;
using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Domain.Entities;
using CqrsMediatr.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace CqrsMediatr.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator, IUnitOfWork unityOfWork)
    {
        _mediator = mediator;

    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var query = new GetMembersQuery();
        var members = await _mediator.Send(query);
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(int id)
    {
        var query = new GetMemberByIdQuery { Id = id };
        var member = await _mediator.Send(query);

        return member != null ? Ok(member) : NotFound("Member not found.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateMemberCommand command)
    {
        var createdMember = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, UpdateMemberCommand command)
    {
        command.Id = id;
        var updatedMember = await _mediator.Send(command);

        return updatedMember != null ? Ok(updatedMember) : NotFound("Member not found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var command = new DeleteMemberCommand { Id = id };

        var deletedMember = await _mediator.Send(command);

        return deletedMember != null ? Ok(deletedMember) : NotFound("Member not found");
    }
}
