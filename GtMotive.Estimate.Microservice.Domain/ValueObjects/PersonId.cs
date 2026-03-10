using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Value Object that encapsulates a person identifier (DNI).
    /// Validates that the value is not empty on construction.
    /// </summary>
    public readonly record struct PersonId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonId"/> class.
        /// </summary>
        /// <param name="value">The string value of the person identifier.</param>
        /// <exception cref="InvalidPersonIdException">Thrown when <paramref name="value"/> is null or whitespace.</exception>
        public PersonId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidPersonIdException("PersonId cannot be empty.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the string value of this person identifier.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the string representation of the identifier.
        /// </summary>
        /// <returns>The string representation of the person identifier.</returns>
        public override string ToString() => Value;
    }
}
