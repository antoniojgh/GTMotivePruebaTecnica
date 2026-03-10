using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Collections
{
    /// <summary>
    /// First-Class Collection of Vehicle entities.
    /// Encapsulates the list of vehicles and provides domain-level filtering behavior.
    /// Per the README pattern: this class only contains the collection — no other fields.
    /// </summary>
    public sealed class VehicleFleet
    {
        private readonly List<Vehicle> _vehicles = new();

        /// <summary>
        /// Gets all vehicles in the collection.
        /// </summary>
        public IReadOnlyCollection<Vehicle> All
            => new ReadOnlyCollection<Vehicle>(_vehicles);

        /// <summary>
        /// Adds a vehicle to the collection.
        /// Protects the invariant: a fleet cannot contain duplicate vehicles.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <exception cref="ArgumentException">Thrown when the vehicle already exists in the fleet.</exception>
        public void Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (_vehicles.Any(v => v.Id == vehicle.Id))
            {
                throw new ArgumentException(
                    $"Vehicle '{vehicle.Id}' already exists in the fleet.",
                    nameof(vehicle));
            }

            _vehicles.Add(vehicle);
        }

        /// <summary>
        /// Returns only the vehicles that are currently available for renting.
        /// Encapsulates availability filtering inside the collection (domain behavior).
        /// </summary>
        /// <returns>A read-only collection of available vehicles.</returns>
        public IReadOnlyCollection<Vehicle> GetOnlyAvailable()
            => _vehicles.Where(v => v.IsAvailable).ToList().AsReadOnly();
    }
}
