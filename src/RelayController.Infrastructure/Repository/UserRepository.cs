using Microsoft.EntityFrameworkCore;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Infrastructure.Context;

namespace RelayController.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly RelayControllerContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(RelayControllerContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<OperationResult> AddAsync(User user, string plainPassword, CancellationToken cancellationToken)
    {
        _passwordHasher.CreatePasswordHash(plainPassword, out var hash, out var salt);
        user.SetPassword(hash, salt);

        await _context.Users.AddAsync(user, cancellationToken);
        return OperationResult.Success();
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> VerifyPasswordAsync(User user, string plainPassword, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_passwordHasher.VerifyPassword(plainPassword, user.PasswordHash, user.PasswordSalt));
    }

    public void Update(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(id,cancellationToken);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }
}