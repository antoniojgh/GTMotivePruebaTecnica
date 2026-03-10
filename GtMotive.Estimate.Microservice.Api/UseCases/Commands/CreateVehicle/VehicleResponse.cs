using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle
{
    /// <summary>Vehicle response DTO.</summary>
    public sealed class VehicleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleResponse"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <param name="isAvailable">Whether the vehicle is available.</param>
        public VehicleResponse(Guid vehicleId, string brand, string model, DateTime manufactureDate, bool isAvailable)
        {
            VehicleId = vehicleId;
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
            IsAvailable = isAvailable;
        }

        /// <summary>Gets the vehicle unique identifier.</summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>Gets the vehicle brand.</summary>
        [Required]
        public string Brand { get; }

        /// <summary>Gets the vehicle model.</summary>
        [Required]
        public string Model { get; }

        /// <summary>Gets the vehicle manufacture date.</summary>
        [Required]
        public DateTime ManufactureDate { get; }

        /// <summary>Gets a value indicating whether the vehicle is available for renting.</summary>
        [Required]
        public bool IsAvailable { get; }
    }
}
