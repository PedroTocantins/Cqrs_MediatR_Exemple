using MediatR;
using Microsoft.Extensions.Logging;

namespace CqrsMediatr.Application.Members.Commands.Notifications;
public class MemberCreatedSMSHandler : INotificationHandler<MemberCreatedNotification>
{
    private readonly ILogger<MemberCreatedSMSHandler> _logger;

    public MemberCreatedSMSHandler(ILogger<MemberCreatedSMSHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation sms sent for : {notification.Member.FirstName}");
        return Task.CompletedTask;
    }
}
