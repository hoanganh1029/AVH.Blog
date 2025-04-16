using AVBlog.Domain.DbConnections;
using AVBlog.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AVBlog.Infrastructure.DbConnections;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    public readonly string _connection = configuration.GetConnectionString(nameof(AVBlogQueryContext))!;

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connection);
    }
}
