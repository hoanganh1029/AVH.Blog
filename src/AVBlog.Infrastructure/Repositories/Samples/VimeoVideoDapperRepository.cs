using AVBlog.Domain.DbConnections;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using Dapper;

namespace AVBlog.Infrastructure.Repositories.Samples
{
    /// <summary>
    /// The handler is used as POC for using Dapper to get data
    /// Dapper is more convenient and efficient in certain scenarios (e.g. complex query, report).
    /// </summary>
    public class VimeoVideoDapperRepository : IVimeoVideoDapperRepository
    {
        protected readonly IDbConnectionFactory _dbConnectionFactory;
        public VimeoVideoDapperRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<IEnumerable<VimeoVideo>> GetAll()
        {
            using var dbConnection = _dbConnectionFactory.CreateConnection();
            var query = @"Select [Id]
                            ,[VimeoId]
                            ,[Title]
                            ,[PublishedDate]
                            ,[Description]
                            ,[Presenter]
                            ,[VideoType] 
                        from VimeoVideos
                        order by PublishedDate desc";
            return await dbConnection.QueryAsync<VimeoVideo>(query);
        }
    }
}
