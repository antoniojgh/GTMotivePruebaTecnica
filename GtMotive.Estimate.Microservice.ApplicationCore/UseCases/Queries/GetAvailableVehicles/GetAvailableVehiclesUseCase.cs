using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Collections;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>
    /// Use case for retrieving all available vehicles.
    /// Builds a VehicleFleet (First-Class Collection) and filters with GetOnlyAvailable()
    /// before passing the result to the output port.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The application logger.</param>
    public sealed class GetAvailableVehiclesUseCase(
        IVehicleRepository vehicleRepository,
        IGetAvailableVehiclesOutputPort outputPort,
        IAppLogger<GetAvailableVehiclesUseCase> logger) : IUseCase<GetAvailableVehiclesInput>
    {
        /// <summary>
        /// Executes the GetAvailableVehicles use case.
        /// </summary>
        /// <param name="input">The use case input.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            var vehicles = await vehicleRepository.GetAllAsync();

            var fleet = new VehicleFleet();
            foreach (var vehicle in vehicles)
            {
                fleet.Add(vehicle);
            }

            var available = fleet.GetOnlyAvailable();

            logger.LogInformation("Returning {Count} available vehicles", available.Count);

            outputPort.StandardHandle(new GetAvailableVehiclesOutput(available));
        }
    }
}
