using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// MediatR handler for the RentVehicle use case.
    /// Builds the input, executes the use case and returns the presenter.
    /// </summary>
    /// <param name="useCase">The RentVehicle use case.</param>
    /// <param name="presenter">The RentVehicle presenter.</param>
    public sealed class RentVehicleHandler(IUseCase<RentVehicleInput> useCase, RentVehiclePresenter presenter) : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.VehicleId, request.PersonId);
            await useCase.Execute(input);
            return presenter;
        }
    }
}
