using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle
{
    /// <summary>Vehicle response DTO.</summary>
    public sealed class VehicleResponse(Guid vehicleId, string brand, string model, DateTime manufactureDate, bool isAvailable)
    {
        /// <summary>Gets the vehicle unique identifier.</summary>
        [Required]
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the vehicle brand.</summary>
        [Required]
        public string Brand { get; } = brand;

        /// <summary>Gets the vehicle model.</summary>
        [Required]
        public string Model { get; } = model;

        /// <summary>Gets the vehicle manufacture date.</summary>
        [Required]
        public DateTime ManufactureDate { get; } = manufactureDate;

        /// <summary>Gets a value indicating whether the vehicle is available for renting.</summary>
        [Required]
        public bool IsAvailable { get; } = isAvailable;
    }
}
