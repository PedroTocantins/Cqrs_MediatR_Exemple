using MediatR;
using Microsoft.Extensions.Logging;

namespace CqrsMediatr.Application.Members.Commands.Notifications;
public class MemberCreatedEmailHandler : INotificationHandler<MemberCreatedNotification>
{
    private readonly ILogger<MemberCreatedEmailHandler> _logger;

    public MemberCreatedEmailHandler(ILogger<MemberCreatedEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation email sento for : {notification.Member.LastName}");

        return Task.CompletedTask;
    }
}
