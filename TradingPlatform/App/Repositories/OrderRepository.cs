using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common.Orders.Dto;
using TradingPlatform.Common.Orders.Dto.Request;

namespace TradingPlatform.App.Repositories
{
    /// <inheritdoc/>
    public sealed class OrderRepository : IOrderRepository
    {
        private IConfiguration Configuration { get; }
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <inheritdoc/>
        public async Task<List<OrderDto>> GetUserOrderAsync(int id)
        {
            var sql = @$"
SELECT
       IdUser 
      ,p.Name as {nameof(OrderDto.ProductName)}
      ,o.Cost
      ,Commission
FROM [dbo].[Order] o
Left join Product p
on o.IdProduct = p.Id
Where o.IdUser = {id}";

            using IDbConnection db = new SqlConnection(_connectionString);
            var oder = (await db.QueryAsync<OrderDto>(sql)).ToList();
            
            return oder;
        }

        /// <inheritdoc/>
        public async Task<string> BuyAsync(int id)
        {
            var selectKey = $@"SELECT TOP 1 ProductKey From [dbo].[ProductKey]
WHERE id = {id}";

            var sql = $@"
DELETE FROM [dbo].[ProductKey]
    WHERE ProductKey = (SELECT TOP 1 ProductKey FROM ProductKey
WHERE id = {id});

UPDATE [dbo].[Product]
   SET [Count] = (select count-1 from [Product] where id = {id})
WHERE Id = {id}";

            using IDbConnection db = new SqlConnection(_connectionString);
            var key = await db.ExecuteScalarAsync<string>(selectKey);
            await db.ExecuteAsync(sql);

            return key;
        }
        
        ///<inheritdoc />
        public async Task CreateOrderAsync(BuyRequest request)
        {
            var selectCommission = $@"select top 1 Commission from MarketCommission
order by Version desc";
                
            using IDbConnection db = new SqlConnection(_connectionString);
            var commission = await db.ExecuteScalarAsync<int>(selectCommission);
            var sql = $@"
INSERT INTO [dbo].[Order]
           ([IdProduct]
           ,[Cost]
           ,[Commission]
           ,[IdUser])
VALUES ({request.ProductId}, {request.Cost}, {commission} , {request.IdUser})";
            await db.ExecuteAsync(sql);
        }

        ///<inheritdoc />
        public async Task SetCommissionAsync(int commission)
        {
            var sql = $@"Insert into MarketCommission (Commission) VALUES ({commission})";
            using IDbConnection db = new SqlConnection(_connectionString);
            await db.ExecuteAsync(sql);
        }
    }
}
