using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CqrsMediatr.Application.Members.Commands;
public sealed class DeleteMemberCommand : IRequest<Member>
{
    public int Id { get; set; }

    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var deletedMember = await _unitOfWork.MemberRepository.GetMemberById(request.Id);

            if(deletedMember is null) 
                throw new InvalidOperationException("Member not found");

            await _unitOfWork.MemberRepository.DeleteMember(deletedMember.Id);
            await _unitOfWork.CommitAssync();

            return deletedMember;

        }
    }
}
