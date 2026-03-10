using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// Presenter for the RentVehicle use case.
    /// Implements IWebApiPresenter and IRentVehicleOutputPort to translate
    /// use case output into an HTTP ActionResult.
    /// </summary>
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(RentVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new RentalResponse(
                output.RentalId,
                output.VehicleId,
                output.PersonId,
                output.StartDate);

            ActionResult = new OkObjectResult(response);
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
            => ActionResult = new NotFoundObjectResult(message);
    }
}
