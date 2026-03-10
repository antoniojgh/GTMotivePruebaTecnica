using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Value Object that uniquely identifies a Vehicle.
    /// </summary>
    public readonly record struct VehicleId(Guid Value)
    {
        /// <summary>
        /// Creates a new VehicleId with a new unique identifier.
        /// </summary>
        /// <returns>A new <see cref="VehicleId"/> instance.</returns>
        public static VehicleId New() => new(Guid.NewGuid());

        /// <summary>
        /// Returns the string representation of the identifier.
        /// </summary>
        /// <returns>The string representation of the vehicle identifier.</returns>
        public override string ToString() => Value.ToString();
    }
}
