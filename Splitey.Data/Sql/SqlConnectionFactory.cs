using Microsoft.Data.SqlClient;

namespace Splitey.Data.Sql;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public SqlConnection Create() => new SqlConnection(connectionString);
}