using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// Presenter for the CreateVehicle use case.
    /// Implements IWebApiPresenter and ICreateVehicleOutputPort to translate
    /// use case output into an HTTP ActionResult.
    /// </summary>
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(CreateVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new VehicleResponse(
                output.VehicleId,
                output.Brand,
                output.Model,
                output.ManufactureDate,
                output.IsAvailable);

            ActionResult = new CreatedResult(
                new Uri($"/api/vehicles/{response.VehicleId}", UriKind.Relative),
                response);
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
            => ActionResult = new NotFoundObjectResult(message);
    }
}
