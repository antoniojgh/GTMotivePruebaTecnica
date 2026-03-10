using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate Root representing a Vehicle in the renting system.
    /// Protects domain invariants within its boundaries and defines the transaction scope.
    /// </summary>
    public abstract class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="brand">The vehicle brand.</param>
        /// <param name="model">The vehicle model.</param>
        /// <param name="manufactureDate">The vehicle manufacture date.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="brand"/> or <paramref name="model"/> is null or whitespace.
        /// </exception>
        /// <exception cref="VehicleManufactureDateInFutureException">
        /// Thrown when <paramref name="manufactureDate"/> is in the future.
        /// </exception>
        /// <exception cref="VehicleManufactureDateTooOldException">
        /// Thrown when the manufacture date is older than 5 years.
        /// </exception>
        protected Vehicle(string brand, string model, DateTime manufactureDate)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Brand cannot be empty.", nameof(brand));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Model cannot be empty.", nameof(model));
            }

            // Use Date to compare only dates without a time component
            var today = DateTime.UtcNow.Date;

            if (manufactureDate.Date > today)
            {
                throw new VehicleManufactureDateInFutureException(
                    $"Vehicle manufacture date '{manufactureDate:yyyy-MM-dd}' cannot be in the future.");
            }

            if (manufactureDate.Date < today.AddYears(-5))
            {
                throw new VehicleManufactureDateTooOldException(
                    $"Vehicle manufacture date '{manufactureDate:yyyy-MM-dd}' is older than 5 years and cannot be registered.");
            }

            Id = VehicleId.New();
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
            IsAvailable = true;
        }

        /// <summary>Gets or sets the vehicle unique identifier.</summary>
        public VehicleId Id { get; protected set; }

        /// <summary>Gets or sets the vehicle brand.</summary>
        public string Brand { get; protected set; }

        /// <summary>Gets or sets the vehicle model.</summary>
        public string Model { get; protected set; }

        /// <summary>Gets or sets the vehicle manufacture date.</summary>
        public DateTime ManufactureDate { get; protected set; }

        /// <summary>Gets or sets a value indicating whether the vehicle is available for renting.</summary>
        public bool IsAvailable { get; protected set; }

        /// <summary>
        /// Marks the vehicle as rented (not available).
        /// Protects the invariant: a vehicle that is already rented cannot be rented again.
        /// </summary>
        /// <exception cref="VehicleNotAvailableException">
        /// Thrown when the vehicle is already rented.
        /// </exception>
        public void MarkAsRented()
        {
            if (!IsAvailable)
            {
                throw new VehicleNotAvailableException(
                    $"Vehicle '{Id}' is already rented and cannot be rented again.");
            }

            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as available again.
        /// Protects the invariant: a vehicle that is already available cannot be returned.
        /// </summary>
        /// <exception cref="VehicleAlreadyAvailableException">
        /// Thrown when the vehicle is already available.
        /// </exception>
        public void MarkAsAvailable()
        {
            if (IsAvailable)
            {
                throw new VehicleAlreadyAvailableException(
                    $"Vehicle '{Id}' is already available.");
            }

            IsAvailable = true;
        }
    }
}
