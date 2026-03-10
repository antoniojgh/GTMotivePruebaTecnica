using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>Get available vehicles request.</summary>
    public sealed class GetAvailableVehiclesRequest : IRequest<IWebApiPresenter>
    {
    }
}
