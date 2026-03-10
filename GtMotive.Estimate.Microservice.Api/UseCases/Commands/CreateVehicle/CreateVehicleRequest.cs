using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle
{
    /// <summary>Create vehicle request.</summary>
    public sealed class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>Gets the vehicle brand.</summary>
        [Required]
        public string Brand { get; init; } = string.Empty;

        /// <summary>Gets the vehicle model.</summary>
        [Required]
        public string Model { get; init; } = string.Empty;

        /// <summary>Gets the vehicle manufacture date.</summary>
        [Required]
        required public DateTime ManufactureDate { get; init; }
    }
}
