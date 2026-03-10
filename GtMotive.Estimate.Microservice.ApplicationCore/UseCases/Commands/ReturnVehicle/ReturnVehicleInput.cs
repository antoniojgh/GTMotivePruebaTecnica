using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// Input message for the ReturnVehicle use case.
    /// </summary>
    /// <param name="vehicleId">The identifier of the vehicle to return.</param>
    public sealed class ReturnVehicleInput(Guid vehicleId) : IUseCaseInput
    {
        /// <summary>Gets the vehicle identifier.</summary>
        public Guid VehicleId { get; } = vehicleId;
    }
}
