using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RelayController.Application.Common.Enums;
using RelayController.Application.UseCases.Commands.BoardCommands.CreateRelayControllerBoard;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivate;
using RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;
using RelayController.Application.UseCases.Queries.GetRelayControllerBoard;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Messaging;
using RelayController.Infrastructure.Messaging.Models;

namespace RelayController.Infrastructure.BackgroundServices;

public class RabbitMqBackgroundService: BackgroundService
{
    private readonly IMessageBusService _messageBus;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqBackgroundService(IMessageBusService messageBus, ISender sender, IServiceProvider serviceProvider)
    {
        _messageBus = messageBus;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _messageBus.Subscribe("relay.create", async message =>
        {
            if (message.Equals("create"))
            {
                var command = new CreateRelayControllerBoardCommand
                {
                    IsActive = true,
                    IsEnable = true,
                    StartTime = DateTime.Now,
                    EndTime = null,
                    Repeat = RepeatDto.DoNoRepeat
                };
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                    var response = await sender.Send(command, cancellationToken);
                    await _messageBus.Publish(response,"relay.create");
                }
                catch (Exception ex)
                {
                    //TODO:
                }
            }
        });
        
        Dictionary<Guid, DateTime> _lastUpdates = new();
      
        await _messageBus.Subscribe("relay.live", async message =>
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };
            var liveMessage = JsonSerializer.Deserialize<LiveMessage>(message, options);
            if (liveMessage != null &&  liveMessage.IsLive)
            {
                var query = new GetRelayControllerBoardQuery
                {
                    Id = liveMessage.Id,
                };
                using var scope = _serviceProvider.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                
                var response = await sender.Send(query, cancellationToken);
                
                if (!response.IsActive)
                {
                    var command = new ToggleActivateCommand
                    {
                        Id = liveMessage.Id,
                        IsActive = true
                    };
        
                    await sender.Send(command, cancellationToken); 
                }

                if (response.IsEnable != liveMessage.IsEnable)
                {
                    var command = new ToggleEnableCommand
                    {
            
                        Id = liveMessage.Id,
                        IsEnable = liveMessage.IsEnable
                    };
                    await sender.Send(command, cancellationToken); 
                }
                _lastUpdates[liveMessage.Id] = DateTime.Now;
            }
        });
        
        while (!cancellationToken.IsCancellationRequested)
        {
            using var repositoryScope = _serviceProvider.CreateScope();
            var relayControllerBoardRepository = repositoryScope.ServiceProvider.GetRequiredService<IRelayControllerBoardRepository>();
            var relayControllerBoards = await relayControllerBoardRepository.GetAllActiveAsync(cancellationToken);
            
            foreach (var board in relayControllerBoards)
            {
                if (!_lastUpdates.ContainsKey(board.Id))
                {
                    _lastUpdates.Add(board.Id, DateTime.Now);
                }
            }
            foreach (var (id, lastUpdate) in _lastUpdates)
            {
                if (DateTime.Now - lastUpdate > TimeSpan.FromMinutes(3))
                {
                    
                    var command = new ToggleActivateCommand
                    {
                        Id = id,
                        IsActive = false
                    };
        
                    using var scope = _serviceProvider.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                    await sender.Send(command, cancellationToken);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
        }
    }
}