using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// Use case for creating a new vehicle.
    /// Invokes Unit of Work to persist changes.
    /// </summary>
    /// <param name="vehicleFactory">The vehicle factory.</param>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="outputPort">The output port.</param>
    /// <param name="logger">The application logger.</param>
    public sealed class CreateVehicleUseCase(
        IVehicleFactory vehicleFactory,
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork,
        ICreateVehicleOutputPort outputPort,
        IAppLogger<CreateVehicleUseCase> logger) : IUseCase<CreateVehicleInput>
    {
        /// <summary>
        /// Executes the CreateVehicle use case.
        /// </summary>
        /// <param name="input">The use case input.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicle = vehicleFactory.NewVehicle(input.Brand, input.Model, input.ManufactureDate);
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.Save();

            var output = new CreateVehicleOutput(
                vehicle.Id.Value,
                vehicle.Brand,
                vehicle.Model,
                vehicle.ManufactureDate,
                vehicle.IsAvailable);

            outputPort.StandardHandle(output);

            logger.LogInformation("Vehicle {VehicleId} created successfully", vehicle.Id.Value);
        }
    }
}
