using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when attempting to return a vehicle that is already available.
    /// </summary>
    public class VehicleAlreadyAvailableException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyAvailableException"/> class.
        /// </summary>
        public VehicleAlreadyAvailableException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyAvailableException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleAlreadyAvailableException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAlreadyAvailableException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleAlreadyAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
