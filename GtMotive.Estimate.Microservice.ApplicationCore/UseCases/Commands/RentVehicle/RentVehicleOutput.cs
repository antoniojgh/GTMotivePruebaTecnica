using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// Output message for the RentVehicle use case.
    /// </summary>
    public sealed class RentVehicleOutput(Guid rentalId, Guid vehicleId, string personId, DateTime startDate) : IUseCaseOutput
    {
        /// <summary>Gets the rental unique identifier.</summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>Gets the vehicle identifier.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets the person identifier.</summary>
        public string PersonId { get; } = personId;

        /// <summary>Gets the rental start date.</summary>
        public DateTime StartDate { get; } = startDate;
    }
}
