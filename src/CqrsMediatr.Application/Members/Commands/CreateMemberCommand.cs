using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsMediatr.Application.Members.Commands;
public class CreateMemberCommand : IRequest<Member>
{
    public string? FirstName { get;  set; }
    public string? LastName { get;  set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }

    public sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var newMember = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);

            await _unitOfWork.MemberRepository.AddMember(newMember);
            await _unitOfWork.CommitAssync();

            return newMember;
        }
    }

}
