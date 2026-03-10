using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Entity representing a vehicle rental agreement.
    /// Mutable, identified by RentalId.
    /// </summary>
    public abstract class Rental(VehicleId vehicleId, PersonId personId)
    {
        /// <summary>Gets or sets the rental unique identifier.</summary>
        public RentalId Id { get; protected set; } = RentalId.New();

        /// <summary>Gets or sets the identifier of the rented vehicle.</summary>
        public VehicleId VehicleId { get; protected set; } = vehicleId;

        /// <summary>Gets or sets the identifier of the person who rented the vehicle.</summary>
        public PersonId PersonId { get; protected set; } = personId;

        /// <summary>Gets or sets the date when the rental started.</summary>
        public DateTime StartDate { get; protected set; } = DateTime.UtcNow;

        /// <summary>Gets or sets the date when the rental was closed (vehicle returned). Null while rental is active.</summary>
        public DateTime? EndDate { get; protected set; }

        /// <summary>Gets or sets a value indicating whether the rental is currently active.</summary>
        public bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Closes the rental, marking it as inactive and recording the end date.
        /// Protects the invariant: a rental that is already closed cannot be closed again.
        /// </summary>
        /// <exception cref="RentalAlreadyClosedException">
        /// Thrown when the rental is already closed.
        /// </exception>
        public void Close()
        {
            if (!IsActive)
            {
                throw new RentalAlreadyClosedException(
                    $"Rental '{Id}' is already closed.");
            }

            IsActive = false;
            EndDate = DateTime.UtcNow;
        }
    }
}
