using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle
{
    /// <summary>Return vehicle response DTO.</summary>
    /// <param name="vehicleId">The vehicle identifier.</param>
    /// <param name="returnDate">The date the vehicle was returned.</param>
    public sealed class ReturnVehicleResponse(Guid vehicleId, DateTime returnDate)
    {
        /// <summary>Gets the vehicle identifier.</summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the date the vehicle was returned.</summary>
        [Required]
        public DateTime ReturnDate { get; } = returnDate;
    }
}
