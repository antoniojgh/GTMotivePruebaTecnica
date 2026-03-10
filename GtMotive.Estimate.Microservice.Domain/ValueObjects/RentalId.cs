using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Value Object that uniquely identifies a Rental.
    /// </summary>
    public readonly record struct RentalId(Guid Value)
    {
        /// <summary>
        /// Creates a new RentalId with a new unique identifier.
        /// </summary>
        /// <returns>A new <see cref="RentalId"/> instance.</returns>
        public static RentalId New() => new(Guid.NewGuid());

        /// <summary>
        /// Returns the string representation of the identifier.
        /// </summary>
        /// <returns>The string representation of the rental identifier.</returns>
        public override string ToString() => Value.ToString();
    }
}
