namespace RelayController.Application.UseCases.Queries.UserBoardQueries.GetAllByUser;

public record GetAllByUserResponse
{
    public Guid UserId { get; init; }
    public List<BoardInfo> Boards { get; init; } = [];
}

public record BoardInfo
{
    public Guid RelayControllerBoardId { get; init; }
    
    public string CustomName {get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}
