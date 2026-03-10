#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Vehicle aggregate roots.
    /// Availability filtering is handled by <see cref="Collections.VehicleFleet"/>,
    /// so no GetAvailableAsync method is defined here.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The vehicle if found; otherwise null.</returns>
        Task<Vehicle?> GetByIdAsync(VehicleId id);

        /// <summary>
        /// Gets all vehicles in the repository.
        /// </summary>
        /// <returns>A read-only list of all vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAllAsync();

        /// <summary>
        /// Updates an existing vehicle in the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Vehicle vehicle);
    }
}
