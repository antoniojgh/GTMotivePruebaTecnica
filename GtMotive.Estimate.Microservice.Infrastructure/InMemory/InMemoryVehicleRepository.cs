#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.Entities;

namespace GtMotive.Estimate.Microservice.Infrastructure.InMemory
{
    /// <summary>
    /// In-memory implementation of IVehicleRepository using a ConcurrentDictionary.
    /// Registered as Singleton to ensure the same dictionary persists for the entire process lifetime.
    /// All read and write operations work with clones of the stored entities to avoid reference leaks:
    /// mutations made by callers do not affect the stored state unless UpdateAsync is explicitly called,
    /// mirroring the isolation semantics of a real database-backed repository.
    /// </summary>
    public sealed class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly ConcurrentDictionary<Guid, VehicleEntity> _vehicles = new();

        /// <summary>
        /// Adds a vehicle to the in-memory store. Stores a clone to own the copy independently.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A completed task.</returns>
        public Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            _vehicles[vehicle.Id.Value] = ((VehicleEntity)vehicle).Clone();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves a clone of the vehicle with the given identifier, or null if not found.
        /// The caller receives an independent copy; mutations will not affect the stored state.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>A cloned vehicle if found; otherwise null.</returns>
        public Task<Vehicle?> GetByIdAsync(VehicleId id)
        {
            return _vehicles.TryGetValue(id.Value, out var entity)
                ? Task.FromResult<Vehicle?>(entity.Clone())
                : Task.FromResult<Vehicle?>(null);
        }

        /// <summary>
        /// Retrieves clones of all vehicles in the in-memory store.
        /// </summary>
        /// <returns>A read-only list of cloned vehicles.</returns>
        public Task<IReadOnlyList<Vehicle>> GetAllAsync()
        {
            IReadOnlyList<Vehicle> list = _vehicles.Values
                .Select(e => (Vehicle)e.Clone())
                .ToList()
                .AsReadOnly();

            return Task.FromResult(list);
        }

        /// <summary>
        /// Replaces the stored vehicle with a clone of the provided instance,
        /// persisting the latest state independently of the caller's reference.
        /// Throws if the vehicle does not already exist in the store.
        /// </summary>
        /// <param name="vehicle">The vehicle with updated state.</param>
        /// <returns>A completed task.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the vehicle is not found in the store.</exception>
        public Task UpdateAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (!_vehicles.ContainsKey(vehicle.Id.Value))
            {
                throw new InvalidOperationException($"Vehicle with id '{vehicle.Id.Value}' was not found in the store.");
            }

            _vehicles[vehicle.Id.Value] = ((VehicleEntity)vehicle).Clone();
            return Task.CompletedTask;
        }
    }
}
