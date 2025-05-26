using RelayController.Domain.Common;

namespace RelayController.Domain.Aggregates.UserAggregates;

public interface IUserRepository
{
   Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
   Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

   Task<OperationResult> AddAsync(User user, string rawPassword, CancellationToken cancellationToken);

   Task<bool> VerifyPasswordAsync(User user, string rawPassword, CancellationToken cancellationToken);
   
   Task ChangePasswordAsync(User user, string newPassword, CancellationToken cancellationToken);

   void Update(User user, CancellationToken cancellationToken);
   Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
