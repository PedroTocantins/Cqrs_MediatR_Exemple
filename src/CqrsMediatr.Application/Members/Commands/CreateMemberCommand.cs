using CqrsMediatr.Application.Members.Commands.Notifications;
using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsMediatr.Application.Members.Commands;
public class CreateMemberCommand : MemberCommandBase
{
    public sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IValidator<CreateMemberCommand> _validator;
        private readonly IMediator _mediator;

        public CreateMemberCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IValidator<CreateMemberCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            //_validator = validator;
        }

        public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            //_validator.ValidateAndThrow(request);

            var newMember = new Member(
                request.FirstName,
                request.LastName,
                request.Gender,
                request.Email,
                request.IsActive
            );

            await _unitOfWork.MemberRepository.AddMember(newMember);
            await _unitOfWork.CommitAssync();

            await _mediator.Publish(new MemberCreatedNotification(newMember), cancellationToken);

            return newMember;
        }
    }

}
