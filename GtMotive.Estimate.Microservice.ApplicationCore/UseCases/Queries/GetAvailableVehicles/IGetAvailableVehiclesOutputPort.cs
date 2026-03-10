namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>
    /// Output port for the GetAvailableVehicles use case.
    /// </summary>
    public interface IGetAvailableVehiclesOutputPort :
        IOutputPortStandard<GetAvailableVehiclesOutput>,
        IOutputPortNotFound
    {
    }
}
