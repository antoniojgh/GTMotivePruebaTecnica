using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when attempting to close a rental that is already closed.
    /// </summary>
    public class RentalAlreadyClosedException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalAlreadyClosedException"/> class.
        /// </summary>
        public RentalAlreadyClosedException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalAlreadyClosedException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RentalAlreadyClosedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalAlreadyClosedException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RentalAlreadyClosedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
