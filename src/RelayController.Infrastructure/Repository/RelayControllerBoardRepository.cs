using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace RelayController.Infrastructure.Repository;

public class RelayControllerBoardRepository : IRelayControllerBoardRepository
{
    private readonly RelayControllerContext _context;

    public RelayControllerBoardRepository(RelayControllerContext context)
    {
        _context = context;
    }

    public async Task<RelayControllerBoard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.RelayControllerBoards.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<RelayControllerBoard>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _context.RelayControllerBoards.Where(x => x.IsActive == true).ToListAsync(cancellationToken);
    }
    
    public async Task AddAsync(RelayControllerBoard controllerBoard, CancellationToken cancellationToken)
    {
        await _context.RelayControllerBoards.AddAsync(controllerBoard, cancellationToken);
    }

    public void Update(RelayControllerBoard controllerBoard, CancellationToken cancellationToken)
    {
        _context.RelayControllerBoards.Update(controllerBoard);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var controllerBoard = await GetByIdAsync(id, cancellationToken);
        if (controllerBoard != null)
        {
            _context.RelayControllerBoards.Remove(controllerBoard);
        }
    }
}
