namespace RelayController.Domain.Aggregates.RelayControllerAggregates;

public interface IRelayControllerRepository
{
    Task<RelayController?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(RelayController boiler, CancellationToken cancellationToken);
    void Update(RelayController boiler, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<RelayController>> GetAllActiveAsync(CancellationToken cancellationToken);
}