using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle.
    /// Delegates business rule validation to VehicleService (Domain Service).
    /// Invokes Unit of Work to persist changes.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="vehicleService">The vehicle domain service.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The application logger.</param>
    public sealed class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        VehicleService vehicleService,
        IUnitOfWork unitOfWork,
        IRentVehicleOutputPort outputPort,
        IAppLogger<RentVehicleUseCase> logger) : IUseCase<RentVehicleInput>
    {
        /// <summary>
        /// Executes the RentVehicle use case.
        /// </summary>
        /// <param name="input">The use case input.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicleId = new VehicleId(input.VehicleId);
            var vehicle = await vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
            {
                logger.LogWarning("Vehicle {VehicleId} not found for rental", input.VehicleId);
                outputPort.NotFoundHandle($"Vehicle with id '{input.VehicleId}' was not found.");
                return;
            }

            var personId = new PersonId(input.PersonId);
            var rental = await vehicleService.RentVehicle(vehicle, personId);

            await unitOfWork.Save();

            var output = new RentVehicleOutput(
                rental.Id.Value,
                rental.VehicleId.Value,
                rental.PersonId.Value,
                rental.StartDate);

            outputPort.StandardHandle(output);

            logger.LogInformation("Vehicle {VehicleId} rented to person {PersonId}", input.VehicleId, input.PersonId);
        }
    }
}
