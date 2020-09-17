using DataComp.Training.Api.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Handlers
{
    public class LoggerUserHandler : INotificationHandler<UserRemovedEvent>
    {
        private readonly ILogger<LoggerUserHandler> logger;

        public LoggerUserHandler(ILogger<LoggerUserHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(UserRemovedEvent @event, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Usunięto użytkownika {@event.User.FullName}");

            return Task.CompletedTask;
        }
    }
}
