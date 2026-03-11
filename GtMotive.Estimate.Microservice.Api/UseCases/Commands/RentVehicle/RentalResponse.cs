using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle
{
    /// <summary>Rental response DTO.</summary>
    /// <param name="rentalId">The rental identifier.</param>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="personId">The person identifier.</param>
    /// <param name="startDate">The rental start date.</param>
    public sealed class RentalResponse(Guid rentalId, Guid vehicleId, string personId, DateTime startDate)
    {
        /// <summary>Gets the rental unique identifier.</summary>
        [Required]
        public Guid RentalId { get; } = rentalId;

        /// <summary>Gets the vehicle identifier.</summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the person identifier (DNI).</summary>
        [Required]
        public string PersonId { get; } = personId;

        /// <summary>Gets the rental start date.</summary>
        [Required]
        public DateTime StartDate { get; } = startDate;
    }
}
