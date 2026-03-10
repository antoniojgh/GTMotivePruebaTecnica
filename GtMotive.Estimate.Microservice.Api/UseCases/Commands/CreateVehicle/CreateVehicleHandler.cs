using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// MediatR handler for the CreateVehicle use case.
    /// Builds the input, executes the use case and returns the presenter.
    /// </summary>
    /// <param name="useCase">The CreateVehicle use case.</param>
    /// <param name="presenter">The CreateVehicle presenter.</param>
    public sealed class CreateVehicleHandler(IUseCase<CreateVehicleInput> useCase, CreateVehiclePresenter presenter) : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(request.Brand, request.Model, request.ManufactureDate);
            await useCase.Execute(input);
            return presenter;
        }
    }
}
