using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// Use case for returning a rented vehicle.
    /// Delegates domain operations to VehicleService (Domain Service).
    /// Invokes Unit of Work to persist changes.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="rentalRepository">The rental repository.</param>
    /// <param name="vehicleService">The vehicle domain service.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The application logger.</param>
    public sealed class ReturnVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        VehicleService vehicleService,
        IUnitOfWork unitOfWork,
        IReturnVehicleOutputPort outputPort,
        IAppLogger<ReturnVehicleUseCase> logger) : IUseCase<ReturnVehicleInput>
    {
        /// <summary>
        /// Executes the ReturnVehicle use case.
        /// </summary>
        /// <param name="input">The use case input.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicleId = new VehicleId(input.VehicleId);

            var vehicle = await vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
            {
                logger.LogWarning("Vehicle {VehicleId} not found for return", input.VehicleId);
                outputPort.NotFoundHandle($"Vehicle with id '{input.VehicleId}' was not found.");
                return;
            }

            var rental = await rentalRepository.GetByVehicleIdAsync(vehicleId);
            if (rental == null)
            {
                logger.LogWarning("No active rental found for vehicle {VehicleId}", input.VehicleId);
                outputPort.NotFoundHandle($"No active rental found for vehicle '{input.VehicleId}'.");
                return;
            }

            await vehicleService.ReturnVehicle(vehicle, rental);
            await unitOfWork.Save();

            var output = new ReturnVehicleOutput(vehicle.Id.Value, rental.EndDate.Value);
            outputPort.StandardHandle(output);

            logger.LogInformation("Vehicle {VehicleId} returned successfully", input.VehicleId);
        }
    }
}
