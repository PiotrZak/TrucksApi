using Dapper;
using Trucks.API.Interfaces;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Trucks.API.Queries
{
	public class TrucksQueries: ITrucksQueries
    {
        private readonly IConfiguration _configuration;

        public TrucksQueries(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Truck> GetTruckMainAsync(int truckId)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            var truck = await con.QueryFirstAsync<Truck>(
                "SELECT \n" +
                "    TruckID,\n" +
                "    UniqueCode,\n" +
                "    Name,\n" +
                "    Status,\n" +
                "    Description\n" +
                "FROM dbo.Trucks\n" +
                "WHERE dbo.Trucks.TruckID = @truckId",
                new { truckId });

            return truck;
        }

        public async Task<IEnumerable<Truck>> GetAllTrucksAsync()
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            var trucks = await con.QueryAsync<Truck>(
                "SELECT \n" +
                "    TruckID,\n" +
                "    UniqueCode,\n" +
                "    Name,\n" +
                "    Status,\n" +
                "    Description\n" +
                "FROM dbo.Trucks");


            return trucks;
        }

        public async Task<bool> UpdateTruckStatusAsync(int truckId, TruckStatus status)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            int rowsAffected = await con.ExecuteAsync(
                "UPDATE dbo.Trucks " +
                "SET Status = @Status " +
                "WHERE TruckID = @TruckId",
                new { TruckId = truckId, Status = status });

            return rowsAffected > 0;
        }

        public async Task<bool> InsertTruckAsync(InsertTruckCommand truck)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            // Generate a random integer ID
            Random random = new Random();
            int randomId = random.Next();

            int rowsAffected = await con.ExecuteAsync(
                "INSERT INTO dbo.Trucks (TruckID, UniqueCode, Name, Status, Description) " +
                "VALUES (@TruckID, @UniqueCode, @Name, @Status, @Description)",
                new
                {
                    TruckID = randomId,
                    truck.UniqueCode,
                    truck.Name,
                    truck.Status,
                    truck.Description
                });

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateTruckAsync(UpdateTruckCommand truck)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            int rowsAffected = await con.ExecuteAsync(
                "UPDATE dbo.Trucks " +
                "SET UniqueCode = @UniqueCode, Name = @Name, Status = @Status, Description = @Description " +
                "WHERE TruckID = @TruckId",
                truck);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTruckAsync(int truckId)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            int rowsAffected = await con.ExecuteAsync(
                "DELETE FROM dbo.Trucks WHERE TruckID = @truckId",
                new { truckId });

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Truck>> GetFilteredTrucksAsync(string? name, TruckStatus? status, string? description)
        {
            var connectionString = _configuration["ConnectionStrings:DBConnection"];

            using var con = new SqlConnection(connectionString);
            await con.OpenAsync();

            var sqlBuilder = new StringBuilder("SELECT * FROM dbo.Trucks WHERE 1=1");

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(name))
            {
                sqlBuilder.Append(" AND Name LIKE '%' + @Name + '%'");
                parameters.Add("@Name", name);
            }

            if (status.HasValue)
            {
                sqlBuilder.Append(" AND Status = @Status");
                parameters.Add("@Status", status.Value);
            }

            if (!string.IsNullOrEmpty(description))
            {
                sqlBuilder.Append(" AND Description LIKE '%' + @Description + '%'");
                parameters.Add("@Description", description);
            }

            var trucks = await con.QueryAsync<Truck>(sqlBuilder.ToString(), parameters);

            return trucks;
        }
    }
}

