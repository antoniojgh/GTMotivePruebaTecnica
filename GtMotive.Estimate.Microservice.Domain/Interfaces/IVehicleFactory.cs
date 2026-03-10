using System;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Factory interface for creating Vehicle aggregate roots.
    /// The concrete implementation resides in the Infrastructure layer.
    /// </summary>
    public interface IVehicleFactory
    {
        /// <summary>
        /// Creates a new Vehicle instance.
        /// </summary>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <returns>A new <see cref="Vehicle"/> instance.</returns>
        Vehicle NewVehicle(string brand, string model, DateTime manufactureDate);
    }
}
