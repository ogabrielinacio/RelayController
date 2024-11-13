namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public interface IRelayControllerBoardRepository
{
    Task<RelayControllerBoard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(RelayControllerBoard controllerBoard, CancellationToken cancellationToken);
    void Update(RelayControllerBoard controllerBoard, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<RelayControllerBoard>> GetAllActiveAsync(CancellationToken cancellationToken);
}