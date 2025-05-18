using RelayController.Domain.Aggregates.UserAggregates;

namespace RelayController.Domain.Common
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
