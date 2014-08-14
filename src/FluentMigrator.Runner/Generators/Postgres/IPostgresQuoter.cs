namespace FluentMigrator.Runner.Generators.Postgres
{
    public interface IPostgresQuoter : IQuoter
    {
        string UnQuoteSchemaName(string quoted);
    }
}
