using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle
{
    /// <summary>Rent vehicle request.</summary>
    public sealed class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>Gets the vehicle identifier.</summary>
        [Required]
        required public Guid VehicleId { get; init; }

        /// <summary>Gets the person identifier (DNI).</summary>
        [Required]
        public string PersonId { get; init; } = string.Empty;
    }
}
