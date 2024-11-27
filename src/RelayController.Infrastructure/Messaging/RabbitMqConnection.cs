
using RabbitMQ.Client;

namespace RelayController.Infrastructure.Messaging
{
    public class RabbitMqConnection
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;

        public RabbitMqConnection(string hostname, string username, string password)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };
        }

        public async Task<IConnection>  GetConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = await _factory.CreateConnectionAsync();
            }

            return _connection;
        }
    }
}
