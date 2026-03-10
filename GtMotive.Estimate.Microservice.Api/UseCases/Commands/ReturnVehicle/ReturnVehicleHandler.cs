using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// MediatR handler for the ReturnVehicle use case.
    /// Builds the input, executes the use case and returns the presenter.
    /// </summary>
    /// <param name="useCase">The ReturnVehicle use case.</param>
    /// <param name="presenter">The ReturnVehicle presenter.</param>
    public sealed class ReturnVehicleHandler(IUseCase<ReturnVehicleInput> useCase, ReturnVehiclePresenter presenter) : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.VehicleId);
            await useCase.Execute(input);
            return presenter;
        }
    }
}
