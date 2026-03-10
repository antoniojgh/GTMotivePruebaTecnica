namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle
{
    /// <summary>
    /// Output port for the RentVehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort :
        IOutputPortStandard<RentVehicleOutput>,
        IOutputPortNotFound
    {
    }
}
