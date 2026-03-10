using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle
{
    /// <summary>Return vehicle request.</summary>
    public sealed class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>Gets the vehicle identifier.</summary>
        [Required]
        required public Guid VehicleId { get; init; }
    }
}
