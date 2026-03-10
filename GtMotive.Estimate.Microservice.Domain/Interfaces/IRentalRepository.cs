#nullable enable

using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Rental entities.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Adds a new rental to the repository.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Gets the active rental for a person, if any.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns>The active rental if the person has one; otherwise null.</returns>
        Task<Rental?> GetActiveByPersonAsync(PersonId personId);

        /// <summary>
        /// Gets the active rental for a vehicle, if any.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The active rental for the vehicle if one exists; otherwise null.</returns>
        Task<Rental?> GetByVehicleIdAsync(VehicleId vehicleId);

        /// <summary>
        /// Updates an existing rental in the repository.
        /// </summary>
        /// <param name="rental">The rental with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Rental rental);
    }
}
