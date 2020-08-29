using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TradingPlatform.App.Entities;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common;
using TradingPlatform.Common.Users;
using TradingPlatform.Common.Users.Dto;
using TradingPlatform.Common.Users.Dto.Request;

namespace TradingPlatform.Repositories
{
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        private IConfiguration Configuration { get; }
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <inheritdoc/>
        public async Task<List<UsersDto>> GetUsersAsync()
        {
            const string sql = @"
SELECT u.Id
      ,r.Name as Role
      ,u.Email
FROM [dbo].[User] u
Left join Role r
on u.IdRole = r.id";

            using IDbConnection db = new SqlConnection(_connectionString);
            var users = (await db.QueryAsync<UsersDto>(sql)).ToList();
            return users;
        }

        /// <inheritdoc/>
        public async Task<UsersDto> GetUserAsync(int id)
        {
            var sql = @$"
SELECT u.Id
      ,r.Name as Role
      ,u.Email
  FROM [dbo].[User] u
  Left join Role r
  on u.IdRole = r.id
Where u.id = {id}";

            using IDbConnection db = new SqlConnection(_connectionString);
            var user = (await db.QueryAsync<UsersDto>(sql)).FirstOrDefault();

            return user;
        }

        /// <inheritdoc/>
        public async Task CreateUserAsync(LoginRequest request)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var sql = @$"Insert Into [dbo].[User] Values(NEXT VALUE FOR UserSequence, 1, '{request.Email}' , '{request.Password}')";

            await db.ExecuteAsync(sql);
        }

        /// <inheritdoc/>
        public async Task UpdateUserAsync(UpdateRequest request)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = @$"
UPDATE [dbo].[User]
   SET [Email] = '{request.Email}'
 WHERE id = {request.Id}";

            await db.ExecuteAsync(sql);
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var sql = $"DELETE FROM [dbo].[User] WHERE Id = {id}";

            await db.ExecuteAsync(sql);
        }

        /// <inheritdoc/>
        public async Task<UserEntity> GetUserByLoginAsync(string login)
        {
            var sql = @$"
SELECT u.Id
      ,r.Name as Role
      ,u.Email
      ,u.Password
  FROM [dbo].[User] u
  Left join Role r
  on u.IdRole = r.id
Where u.Email = '{login}'";

            using IDbConnection db = new SqlConnection(_connectionString);
            var user = (await db.QueryAsync<UserEntity>(sql)).FirstOrDefault();

            return user;
        }
    }
}
