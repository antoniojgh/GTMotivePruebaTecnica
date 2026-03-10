using System;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a PersonId value object is constructed with an empty or whitespace value.
    /// Inherits from DomainException so that BusinessExceptionFilter returns HTTP 400.
    /// </summary>
    public class InvalidPersonIdException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPersonIdException"/> class.
        /// </summary>
        public InvalidPersonIdException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPersonIdException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InvalidPersonIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPersonIdException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidPersonIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
