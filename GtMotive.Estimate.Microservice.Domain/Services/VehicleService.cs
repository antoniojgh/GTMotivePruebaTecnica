using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Services
{
    /// <summary>
    /// Domain Service encapsulating cross-entity business logic for vehicle rental operations.
    /// Located in the Domain layer because it enforces core business invariants
    /// (e.g., "a person cannot have more than one active rental") that are intrinsic
    /// to the renting business, not mere application-flow rules.
    /// Does NOT call IUnitOfWork — that is the responsibility of the Use Case.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository.</param>
    /// <param name="rentalRepository">The rental repository.</param>
    /// <param name="rentalFactory">The rental factory.</param>
    public class VehicleService(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IRentalFactory rentalFactory)
    {
        /// <summary>
        /// Rents a vehicle to a person, enforcing domain business rules.
        /// </summary>
        /// <param name="vehicle">The vehicle to rent.</param>
        /// <param name="personId">The identifier of the person renting the vehicle.</param>
        /// <returns>The newly created rental.</returns>
        /// <exception cref="PersonAlreadyHasActiveRentalException">
        /// Thrown when the person already has an active rental.
        /// </exception>
        /// <exception cref="VehicleNotAvailableException">
        /// Thrown when the vehicle is not available.
        /// </exception>
        public async Task<Rental> RentVehicle(Vehicle vehicle, PersonId personId)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var existing = await rentalRepository.GetActiveByPersonAsync(personId);
            if (existing != null)
            {
                throw new PersonAlreadyHasActiveRentalException(
                    $"Person {personId} already has an active rental.");
            }

            if (!vehicle.IsAvailable)
            {
                throw new VehicleNotAvailableException(
                    $"Vehicle {vehicle.Id} is not available.");
            }

            var rental = rentalFactory.NewRental(vehicle.Id, personId);
            vehicle.MarkAsRented();
            await rentalRepository.AddAsync(rental);
            await vehicleRepository.UpdateAsync(vehicle);
            return rental;
        }

        /// <summary>
        /// Returns a vehicle, closing the associated rental.
        /// </summary>
        /// <param name="vehicle">The vehicle being returned.</param>
        /// <param name="rental">The active rental to close.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ReturnVehicle(Vehicle vehicle, Rental rental)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            ArgumentNullException.ThrowIfNull(rental);

            rental.Close();
            vehicle.MarkAsAvailable();
            await rentalRepository.UpdateAsync(rental);
            await vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
