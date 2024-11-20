using System.Text.Json.Serialization;
using RelayController.Application.Common.Enums;
using MediatR;

namespace RelayController.Application.UseCases.Commands.UpdateRelayControllerBoard;

public record UpdateRelayControllerBoardCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; init; }
    public bool IsEnable { get; init; }
    public bool IsActive { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public RepeatDTO Repeat { get; init; }
}