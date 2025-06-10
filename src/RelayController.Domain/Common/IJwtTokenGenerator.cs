using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Enums;

namespace RelayController.Domain.Common
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, TokenPurpose purpose);
    }
}
