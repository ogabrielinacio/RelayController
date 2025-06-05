using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Messaging;

namespace RelayController.Infrastructure.BackgroundServices;

public class RelayControllerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RelayControllerBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var relayControllerBoardRepository = scope.ServiceProvider.GetRequiredService<IRelayControllerBoardRepository>();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();

                    var relayControllerBoards = await relayControllerBoardRepository.GetAllActiveAsync(cancellationToken);

                    foreach (var board in relayControllerBoards)
                    {
                        var datetime = DateTime.Now;
                        var command = new ToggleEnableCommand
                        {
                            Id = board.Id
                        };

                        if (board.MustBeOn(datetime))
                        {
                            await sender.Send(command with { IsEnable = true }, cancellationToken);
                        }
                        else if (board.MustBeOff(datetime))
                        {
                            await sender.Send(command with { IsEnable = false }, cancellationToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO:
            }

            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }
}
