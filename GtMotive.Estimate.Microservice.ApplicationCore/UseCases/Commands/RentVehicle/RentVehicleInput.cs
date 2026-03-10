using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// Input message for the RentVehicle use case.
    /// </summary>
    /// <param name="vehicleId">The identifier of the vehicle to rent.</param>
    /// <param name="personId">The identifier of the person renting the vehicle.</param>
    public sealed class RentVehicleInput(Guid vehicleId, string personId) : IUseCaseInput
    {
        /// <summary>Gets the vehicle identifier.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the person identifier.</summary>
        public string PersonId { get; } = personId;
    }
}
