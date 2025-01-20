using MongoDB.Driver;
using OrganizandoTudo.Infrastructure.Data.MongoDB.Interfaces;

namespace OrganizandoTudo.Infrastructure.Data.MongoDB.Configuration
{
    public class MongoDbContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        private IClientSessionHandle Session { get; set; }
        private MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IMongoDbSettings _settings;

        public MongoDbContext(IMongoDbSettings settings)
        {
            _settings = settings;
            _commands = new List<Func<Task>>();

            ConfigureMongo();
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
                return;

            MongoClient = new MongoClient(_settings.ConnectionString);
            Database = MongoClient.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public async Task<bool> SaveChangesAsync()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                try
                {
                    await Task.WhenAll(commandTasks);
                    await Session.CommitTransactionAsync();
                    return true;
                }
                catch
                {
                    await Session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public async Task BeginTransactionAsync()
        {
            Session = await MongoClient.StartSessionAsync();
            Session.StartTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            if (Session == null) return;

            await Session.CommitTransactionAsync();
            _commands.Clear();
        }

        public async Task AbortTransactionAsync()
        {
            if (Session == null) return;

            await Session.AbortTransactionAsync();
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
