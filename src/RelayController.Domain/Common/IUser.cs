using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Common
{
    public interface IUser
    {
        public string Id { get; }

        public string Name { get; }

    }
}
