using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a vehicle's manufacture date is older than 5 years.
    /// </summary>
    public class VehicleManufactureDateTooOldException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateTooOldException"/> class.
        /// </summary>
        public VehicleManufactureDateTooOldException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateTooOldException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleManufactureDateTooOldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleManufactureDateTooOldException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleManufactureDateTooOldException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
