using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle
{
    /// <summary>Return vehicle response DTO.</summary>
    public sealed class ReturnVehicleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleResponse"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="returnDate">The date the vehicle was returned.</param>
        public ReturnVehicleResponse(Guid vehicleId, DateTime returnDate)
        {
            VehicleId = vehicleId;
            ReturnDate = returnDate;
        }

        /// <summary>Gets the vehicle identifier.</summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>Gets the date the vehicle was returned.</summary>
        [Required]
        public DateTime ReturnDate { get; }
    }
}
