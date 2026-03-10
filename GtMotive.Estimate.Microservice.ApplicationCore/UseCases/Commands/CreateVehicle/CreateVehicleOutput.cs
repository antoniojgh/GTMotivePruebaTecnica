using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// Output message for the CreateVehicle use case.
    /// </summary>
    public sealed class CreateVehicleOutput(Guid vehicleId, string brand, string model, DateTime manufactureDate, bool isAvailable) : IUseCaseOutput
    {
        /// <summary>Gets the vehicle unique identifier.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the vehicle brand.</summary>
        public string Brand { get; } = brand;

        /// <summary>Gets the vehicle model.</summary>
        public string Model { get; } = model;

        /// <summary>Gets the vehicle manufacture date.</summary>
        public DateTime ManufactureDate { get; } = manufactureDate;

        /// <summary>Gets a value indicating whether the vehicle is available.</summary>
        public bool IsAvailable { get; } = isAvailable;
    }
}
