using System.Data;
using AVBlog.Domain.LifeTime;

namespace AVBlog.Domain.DbConnections;

public interface IDbConnectionFactory : IScopedDependency
{
    public IDbConnection CreateConnection();
}
