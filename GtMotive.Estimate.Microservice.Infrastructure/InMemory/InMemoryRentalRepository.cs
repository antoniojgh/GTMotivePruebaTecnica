#nullable enable

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.Entities;

namespace GtMotive.Estimate.Microservice.Infrastructure.InMemory
{
    /// <summary>
    /// In-memory implementation of IRentalRepository using a ConcurrentDictionary.
    /// Registered as Singleton to ensure the same dictionary persists for the entire process lifetime.
    /// All read and write operations work with clones of the stored entities to avoid reference leaks:
    /// mutations made by callers do not affect the stored state unless UpdateAsync is explicitly called,
    /// mirroring the isolation semantics of a real database-backed repository.
    /// </summary>
    public sealed class InMemoryRentalRepository : IRentalRepository
    {
        private readonly ConcurrentDictionary<Guid, RentalEntity> _rentals = new();

        /// <summary>
        /// Adds a rental to the in-memory store. Stores a clone to own the copy independently.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A completed task.</returns>
        public Task AddAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            _rentals[rental.Id.Value] = ((RentalEntity)rental).Clone();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves a clone of the active rental for a given person, or null if none exists.
        /// The caller receives an independent copy; mutations will not affect the stored state.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns>A cloned active rental if found; otherwise null.</returns>
        public Task<Rental?> GetActiveByPersonAsync(PersonId personId)
        {
            var entity = _rentals.Values
                .FirstOrDefault(r => r.PersonId.Value == personId.Value && r.IsActive);

            return Task.FromResult<Rental?>(entity?.Clone());
        }

        /// <summary>
        /// Retrieves a clone of the active rental for a given vehicle, or null if none exists.
        /// The caller receives an independent copy; mutations will not affect the stored state.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>A cloned active rental if found; otherwise null.</returns>
        public Task<Rental?> GetByVehicleIdAsync(VehicleId vehicleId)
        {
            var entity = _rentals.Values
                .FirstOrDefault(r => r.VehicleId.Value == vehicleId.Value && r.IsActive);

            return Task.FromResult<Rental?>(entity?.Clone());
        }

        /// <summary>
        /// Replaces the stored rental with a clone of the provided instance,
        /// persisting the latest state independently of the caller's reference.
        /// Throws if the rental does not already exist in the store.
        /// </summary>
        /// <param name="rental">The rental with updated state.</param>
        /// <returns>A completed task.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the rental is not found in the store.</exception>
        public Task UpdateAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);

            if (!_rentals.ContainsKey(rental.Id.Value))
            {
                throw new InvalidOperationException($"Rental with id '{rental.Id.Value}' was not found in the store.");
            }

            _rentals[rental.Id.Value] = ((RentalEntity)rental).Clone();
            return Task.CompletedTask;
        }
    }
}
