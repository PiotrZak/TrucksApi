using System;
namespace Trucks.API.Interfaces
{
    public interface ITrucksQueries
    {
        Task<Truck> GetTruckMainAsync(int TruckId);
        Task<IEnumerable<Truck>> GetAllTrucksAsync();
        Task<IEnumerable<Truck>> GetFilteredTrucksAsync(string? Name, TruckStatus? Status, string? Description);

        Task<bool> InsertTruckAsync(InsertTruckCommand Truck);
        Task<bool> UpdateTruckAsync(UpdateTruckCommand Truck);
        Task<bool> DeleteTruckAsync(int TruckId);
        Task<bool> UpdateTruckStatusAsync(int TruckId, TruckStatus Status);
    }
}

