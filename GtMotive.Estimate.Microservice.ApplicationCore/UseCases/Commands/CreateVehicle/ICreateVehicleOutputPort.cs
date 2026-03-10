namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle
{
    /// <summary>
    /// Output port for the CreateVehicle use case.
    /// </summary>
    public interface ICreateVehicleOutputPort :
        IOutputPortStandard<CreateVehicleOutput>,
        IOutputPortNotFound
    {
    }
}
