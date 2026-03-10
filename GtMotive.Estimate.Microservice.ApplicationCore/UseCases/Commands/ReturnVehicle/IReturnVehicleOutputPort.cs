namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle
{
    /// <summary>
    /// Output port for the ReturnVehicle use case.
    /// </summary>
    public interface IReturnVehicleOutputPort :
        IOutputPortStandard<ReturnVehicleOutput>,
        IOutputPortNotFound
    {
    }
}
