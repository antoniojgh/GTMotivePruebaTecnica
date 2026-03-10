using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Queries.GetAvailableVehicles
{
    /// <summary>
    /// MediatR handler for the GetAvailableVehicles use case.
    /// Builds the input, executes the use case and returns the presenter.
    /// </summary>
    /// <param name="useCase">The GetAvailableVehicles use case.</param>
    /// <param name="presenter">The GetAvailableVehicles presenter.</param>
    public sealed class GetAvailableVehiclesHandler(IUseCase<GetAvailableVehiclesInput> useCase, GetAvailableVehiclesPresenter presenter) : IRequestHandler<GetAvailableVehiclesRequest, IWebApiPresenter>
    {
        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(GetAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            var input = new GetAvailableVehiclesInput();
            await useCase.Execute(input);
            return presenter;
        }
    }
}
