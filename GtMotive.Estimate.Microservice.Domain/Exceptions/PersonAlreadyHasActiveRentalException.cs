using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a person attempts to rent a vehicle but already has an active rental.
    /// </summary>
    public class PersonAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public PersonAlreadyHasActiveRentalException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public PersonAlreadyHasActiveRentalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public PersonAlreadyHasActiveRentalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
