using Microsoft.Data.SqlClient;

namespace Splitey.Data.Sql;

public interface ISqlConnectionFactory
{
    SqlConnection Create();
}