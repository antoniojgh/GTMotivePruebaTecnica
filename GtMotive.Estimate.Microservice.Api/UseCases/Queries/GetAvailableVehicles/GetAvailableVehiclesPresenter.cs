using System;
using System.Linq;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>
    /// Presenter for the GetAvailableVehicles use case.
    /// Maps the already-filtered available vehicles to response DTOs.
    /// </summary>
    public sealed class GetAvailableVehiclesPresenter : IWebApiPresenter, IGetAvailableVehiclesOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; } = new StatusCodeResult(500);

        /// <inheritdoc/>
        public void StandardHandle(GetAvailableVehiclesOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var responses = output.AvailableVehicles
                .Select(v => new VehicleAvailableResponse(
                    v.Id.Value,
                    v.Brand,
                    v.Model,
                    v.ManufactureDate,
                    v.IsAvailable))
                .ToList();

            ActionResult = new OkObjectResult(responses);
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
            => ActionResult = new NotFoundObjectResult(message);
    }
}
