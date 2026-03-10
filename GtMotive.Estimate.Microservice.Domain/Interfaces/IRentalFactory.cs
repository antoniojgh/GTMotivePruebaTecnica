using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Factory interface for creating Rental entities.
    /// The concrete implementation resides in the Infrastructure layer.
    /// </summary>
    public interface IRentalFactory
    {
        /// <summary>
        /// Creates a new Rental instance.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle being rented.</param>
        /// <param name="personId">The identifier of the person renting the vehicle.</param>
        /// <returns>A new <see cref="Rental"/> instance.</returns>
        Rental NewRental(VehicleId vehicleId, PersonId personId);
    }
}
