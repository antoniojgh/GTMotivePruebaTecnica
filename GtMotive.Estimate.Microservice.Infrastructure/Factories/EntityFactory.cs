using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.Entities;

namespace GtMotive.Estimate.Microservice.Infrastructure.Factories
{
    /// <summary>
    /// Concrete implementation of the Entity Factory pattern.
    /// Implements both IVehicleFactory and IRentalFactory by creating
    /// the concrete infrastructure entities VehicleEntity and RentalEntity.
    /// </summary>
    public sealed class EntityFactory : IVehicleFactory, IRentalFactory
    {
        /// <summary>
        /// Creates a new Vehicle instance with the given brand, model, and manufacture date.
        /// </summary>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <returns>A new <see cref="Vehicle"/> instance.</returns>
        public Vehicle NewVehicle(string brand, string model, DateTime manufactureDate)
            => new VehicleEntity(brand, model, manufactureDate);

        /// <summary>
        /// Creates a new Rental instance for the given vehicle and person.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle being rented.</param>
        /// <param name="personId">The identifier of the person renting the vehicle.</param>
        /// <returns>A new <see cref="Rental"/> instance.</returns>
        public Rental NewRental(VehicleId vehicleId, PersonId personId)
            => new RentalEntity(vehicleId, personId);
    }
}
