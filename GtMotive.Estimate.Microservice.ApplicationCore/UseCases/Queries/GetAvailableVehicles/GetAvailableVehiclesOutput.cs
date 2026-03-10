using System.Collections.Generic;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>
    /// Output message for the GetAvailableVehicles use case.
    /// Carries only the available vehicles, already filtered by the Use Case
    /// using VehicleFleet.GetOnlyAvailable() (First-Class Collection).
    /// </summary>
    public sealed class GetAvailableVehiclesOutput(IReadOnlyCollection<Vehicle> availableVehicles) : IUseCaseOutput
    {
        /// <summary>Gets the available vehicles.</summary>
        public IReadOnlyCollection<Vehicle> AvailableVehicles { get; } = availableVehicles;
    }
}
