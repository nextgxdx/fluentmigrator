namespace FluentMigrator.Runner.Processors.Postgres
{
    using Generators.Postgres;

    public class PostgresProcessorFactory : MigrationProcessorFactory
    {
        public override IMigrationProcessor Create(string connectionString, IAnnouncer announcer, IMigrationProcessorOptions options)
        {
            var factory = new PostgresDbFactory();
            var connection = factory.CreateConnection(connectionString);
            return new PostgresProcessor(connection,
                new PostgresGenerator(LowerCaseIdentifiers(options.ProviderSwitches)), announcer, options, factory);
        }

        private bool LowerCaseIdentifiers(string options)
        {
            return !string.IsNullOrEmpty(options) &&
                options.ToUpper().Contains("LOWERCASEIDENTIFIERS=TRUE");
        }
    }
}