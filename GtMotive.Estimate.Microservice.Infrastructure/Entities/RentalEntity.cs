using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Infrastructure.Entities
{
    /// <summary>
    /// Concrete implementation of the Rental entity.
    /// Provides the instantiable type required by the Entity Factory pattern.
    /// Domain behavior (Close) is implemented in the base class
    /// with invariant protection at the Entity level.
    /// </summary>
    /// <param name="vehicleId">The identifier of the rented vehicle.</param>
    /// <param name="personId">The identifier of the person renting the vehicle.</param>
    public sealed class RentalEntity(VehicleId vehicleId, PersonId personId) : Rental(vehicleId, personId)
    {
        /// <summary>
        /// Creates a shallow copy of this entity.
        /// Safe as a deep copy because all properties are value types or immutable (readonly record structs, string).
        /// Does not re-execute the domain constructor, so no invariant validation is triggered.
        /// </summary>
        /// <returns>An independent copy of this <see cref="RentalEntity"/>.</returns>
        internal RentalEntity Clone() => (RentalEntity)MemberwiseClone();
    }
}
