using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// Presenter for the ReturnVehicle use case.
    /// Implements IWebApiPresenter and IReturnVehicleOutputPort to translate
    /// use case output into an HTTP ActionResult.
    /// </summary>
    public sealed class ReturnVehiclePresenter : IWebApiPresenter, IReturnVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(ReturnVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new ReturnVehicleResponse(output.VehicleId, output.ReturnDate);
            ActionResult = new OkObjectResult(response);
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
            => ActionResult = new NotFoundObjectResult(message);
    }
}
