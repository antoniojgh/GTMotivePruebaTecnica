using System;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Infrastructure.Entities
{
    /// <summary>
    /// Concrete implementation of the Vehicle aggregate root.
    /// Provides the instantiable type required by the Entity Factory pattern.
    /// Domain behavior (MarkAsRented, MarkAsAvailable) is implemented in the base class
    /// with invariant protection at the Aggregate Root level.
    /// </summary>
    /// <param name="brand">The vehicle brand.</param>
    /// <param name="model">The vehicle model.</param>
    /// <param name="manufactureDate">The vehicle manufacture date.</param>
    public sealed class VehicleEntity(string brand, string model, DateTime manufactureDate)
        : Vehicle(brand, model, manufactureDate)
    {
        /// <summary>
        /// Creates a shallow copy of this entity.
        /// Safe as a deep copy because all properties are value types or immutable (readonly record structs, string).
        /// Does not re-execute the domain constructor, so no invariant validation is triggered.
        /// </summary>
        /// <returns>An independent copy of this <see cref="VehicleEntity"/>.</returns>
        internal VehicleEntity Clone() => (VehicleEntity)MemberwiseClone();
    }
}
