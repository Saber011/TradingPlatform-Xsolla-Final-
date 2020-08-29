using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TradingPlatform.App.Interfaces;
using TradingPlatform.Common.Products.Dto;
using TradingPlatform.Common.Products.Dto.Request;

namespace TradingPlatform.App.Repositories
{
    /// <inheritdoc/>
    public class ProductRepository : IProductRepository
    {
        private IConfiguration Configuration { get; }
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <inheritdoc/>
        public async Task CreateProductAsync(ProductRequest request)
        {
            const string nextId = "Select NEXT VALUE FOR ProductSequence";
            int id;
            using (IDbConnection db1 = new SqlConnection(_connectionString))
            {
                id = await db1.ExecuteScalarAsync<int>(nextId);
            }

            var keyTransform = request.GameKey.Select(x => string.Format($"({id}, '{x}')", x));
            var result = string.Join(",", keyTransform);

            var keyInsert = $"Insert into productKey VALUES{result}";
            var productInsert = $@"INSERT INTO Product (Id, Name, Cost, Description, Count, VendorId)
 VALUES({id},'{request.Name}', {request.Cost}, '{request.Description}', {request.Count}, {request.VendorId});";

            using IDbConnection db = new SqlConnection(_connectionString);
            await db.ExecuteAsync(productInsert);
            await db.ExecuteAsync(keyInsert);
        }

        /// <inheritdoc/>
        public async Task DeleteProductAsync(int id)
        {
            var deleteProduct = $"Delete from Product where id = {id};";
            var deleteProductLey = $"Delete from ProductKey where id = {id};";

            using IDbConnection db = new SqlConnection(_connectionString);
            await db.ExecuteAsync(deleteProduct);
            await db.ExecuteAsync(deleteProductLey);
        }

        /// <inheritdoc/>
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var sql = @$"select 
id as {nameof(ProductDto.Id)},
Name as {nameof(ProductDto.Name)},
Cost as {nameof(ProductDto.Cost)},
Description as {nameof(ProductDto.Description)},
Count as {nameof(ProductDto.Count)},
VendorId as {nameof(ProductDto.VendorId)}
    from Product t
    where t.Id = {id}";

            using IDbConnection db = new SqlConnection(_connectionString);
            var product = (await db.QueryAsync<ProductDto>(sql)).FirstOrDefault();

            return product;
        }

        /// <inheritdoc/>
        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var sql = @$"select 
id as {nameof(ProductDto.Id)},
Name as {nameof(ProductDto.Name)},
Cost as {nameof(ProductDto.Cost)},
Description as {nameof(ProductDto.Description)},
Count as {nameof(ProductDto.Count)},
VendorId as {nameof(ProductDto.VendorId)}
from Product
Where Count >= 1";
            using IDbConnection db = new SqlConnection(_connectionString);
            var products = (await db.QueryAsync<ProductDto>(sql)).ToList();

            return products;
        }

        /// <inheritdoc/>
        public async Task UpdateProductAsync(UpdateProductRequest request)
        {
            var updateProduct = $@"
UPDATE [dbo].[Product]
  set [Name] = '{request.Name}',
  [Cost] = {request.Cost},
  [Description] = '{request.Description}'
Where id = {request.Id};";

            using IDbConnection db = new SqlConnection(_connectionString);
            await db.ExecuteAsync(updateProduct);
        }
    }
}
