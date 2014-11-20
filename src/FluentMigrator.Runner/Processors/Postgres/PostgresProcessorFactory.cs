namespace FluentMigrator.Runner.Processors.Postgres
{
    using System;
    using Generators.Postgres;
    using Pgpass.Net;

    public class PostgresProcessorFactory : MigrationProcessorFactory
    {
        private static string GetPgConnectionString(string connstr)
        {
            var parts = connstr.Substring(7).Split(';');
            if (parts.Length != 3)
                throw new FormatException("Bad pgpass connection string (should be pgpass=server:port;database;user)");
            var pgpass = new Pgpass(parts[0]);
            return pgpass.GetConnectionString(parts[1], parts[2]);
        }

        public override IMigrationProcessor Create(string connectionString, IAnnouncer announcer, IMigrationProcessorOptions options)
        {
            var factory = new PostgresDbFactory();

            var connection =
                factory.CreateConnection(connectionString.StartsWith("pgpass=", StringComparison.OrdinalIgnoreCase)
                    ? GetPgConnectionString(connectionString)
                    : connectionString);

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