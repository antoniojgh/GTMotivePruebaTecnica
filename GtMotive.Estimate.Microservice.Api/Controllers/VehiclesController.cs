using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Queries.GetAvailableVehicles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for vehicle management endpoints.
    /// Delegates all processing to MediatR handlers following the Command pattern.
    /// </summary>
    /// <param name="mediator">The MediatR mediator.</param>
    [ApiController]
    [Route("api/vehicles")]
    public sealed class VehiclesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="request">The create vehicle request.</param>
        /// <returns>HTTP 201 Created with the new vehicle data.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody][Required] CreateVehicleRequest request)
            => (await mediator.Send(request)).ActionResult;

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <returns>HTTP 200 OK with the list of available vehicles.</returns>
        [HttpGet("available")]
        [ProducesResponseType(typeof(IReadOnlyList<VehicleAvailableResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAvailable()
            => (await mediator.Send(new GetAvailableVehiclesRequest())).ActionResult;

        /// <summary>
        /// Rents a vehicle to a person.
        /// </summary>
        /// <param name="request">The rent vehicle request.</param>
        /// <returns>HTTP 200 OK with the rental data.</returns>
        [HttpPost("rent")]
        [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Rent([FromBody][Required] RentVehicleRequest request)
            => (await mediator.Send(request)).ActionResult;

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="request">The return vehicle request.</param>
        /// <returns>HTTP 200 OK with the return data.</returns>
        [HttpPost("return")]
        [ProducesResponseType(typeof(ReturnVehicleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Return([FromBody][Required] ReturnVehicleRequest request)
            => (await mediator.Send(request)).ActionResult;
    }
}
