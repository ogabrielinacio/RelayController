using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RelayController.Application.UseCases.Commands.ToggleEnable;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Messaging;

namespace RelayController.Infrastructure.BackgroundServices;

public class RelayControllerBackgroundService : BackgroundService
{
    private readonly ISender _sender;
    private readonly IServiceProvider _serviceProvider;

    public RelayControllerBackgroundService(ISender sender, IServiceProvider serviceProvider)
    {
        _sender = sender;
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
                            await _sender.Send(command with { IsEnable = true }, cancellationToken);
                        }
                        else if (board.MustBeOff(datetime))
                        {
                            await _sender.Send(command with { IsEnable = false }, cancellationToken);
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
