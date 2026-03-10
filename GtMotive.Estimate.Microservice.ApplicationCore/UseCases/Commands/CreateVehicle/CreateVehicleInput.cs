using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// Input message for the CreateVehicle use case.
    /// </summary>
    /// <param name="brand">The vehicle brand.</param>
    /// <param name="model">The vehicle model.</param>
    /// <param name="manufactureDate">The vehicle manufacture date.</param>
    public sealed class CreateVehicleInput(string brand, string model, DateTime manufactureDate) : IUseCaseInput
    {
        /// <summary>Gets the vehicle brand.</summary>
        public string Brand { get; } = brand;

        /// <summary>Gets the vehicle model.</summary>
        public string Model { get; } = model;

        /// <summary>Gets the vehicle manufacture date.</summary>
        public DateTime ManufactureDate { get; } = manufactureDate;
    }
}
