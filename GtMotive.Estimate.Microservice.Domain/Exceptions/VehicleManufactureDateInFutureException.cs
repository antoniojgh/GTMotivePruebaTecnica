using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle's manufacture date is in the future.
    /// </summary>
    public class VehicleManufactureDateInFutureException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateInFutureException"/> class.
        /// </summary>
        public VehicleManufactureDateInFutureException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateInFutureException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleManufactureDateInFutureException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateInFutureException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleManufactureDateInFutureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
