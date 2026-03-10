using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// Output message for the ReturnVehicle use case.
    /// </summary>
    public sealed class ReturnVehicleOutput(Guid vehicleId, DateTime returnDate) : IUseCaseOutput
    {
        /// <summary>Gets the vehicle identifier.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the date the vehicle was returned.</summary>
        public DateTime ReturnDate { get; } = returnDate;
    }
}
