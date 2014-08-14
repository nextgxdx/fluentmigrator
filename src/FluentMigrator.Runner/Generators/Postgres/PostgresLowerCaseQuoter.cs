using FluentMigrator.Runner.Generators.Generic;

namespace FluentMigrator.Runner.Generators.Postgres
{
    public class PostgresLowerCaseQuoter : GenericQuoter, IPostgresQuoter
    {
        public override string FormatBool(bool value) { return value ? "true" : "false"; }

        public override string QuoteSchemaName(string schemaName)
        {
            if (string.IsNullOrEmpty(schemaName))
                schemaName = "public";
            return base.QuoteSchemaName(schemaName);
        }

        public string UnQuoteSchemaName(string quoted)
        {
            if (string.IsNullOrEmpty(quoted))
                return "public";

            return UnQuote(quoted);
        }

        public override string Quote(string name)
        {
            return base.Quote(name.ToLower());
        }

        public override string QuoteColumnName(string columnName)
        {
            return base.QuoteColumnName(columnName.ToLower());
        }

        public override string QuoteConstraintName(string constraintName)
        {
            return base.QuoteConstraintName(constraintName.ToLower());
        }

        public override string QuoteIndexName(string indexName)
        {
            return base.QuoteIndexName(indexName.ToLower());
        }

        public override string QuoteTableName(string tableName)
        {
            return base.QuoteTableName(tableName.ToLower());
        }

        public override string QuoteSequenceName(string sequenceName)
        {
            return base.QuoteSequenceName(sequenceName.ToLower());
        }

        public override string UnQuote(string name)
        {
            return base.UnQuote(name.ToLower());
        }
    }
}