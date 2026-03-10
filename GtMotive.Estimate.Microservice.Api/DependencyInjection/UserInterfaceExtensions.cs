using GtMotive.Estimate.Microservice.Api.UseCases.Commands.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Commands.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Commands.ReturnVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.Queries.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.CreateVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Commands.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Queries.GetAvailableVehicles;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Api.DependencyInjection
{
    public static class UserInterfaceExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateVehiclePresenter>();
            services.AddScoped<ICreateVehicleOutputPort>(sp => sp.GetRequiredService<CreateVehiclePresenter>());

            services.AddScoped<GetAvailableVehiclesPresenter>();
            services.AddScoped<IGetAvailableVehiclesOutputPort>(sp => sp.GetRequiredService<GetAvailableVehiclesPresenter>());

            services.AddScoped<RentVehiclePresenter>();
            services.AddScoped<IRentVehicleOutputPort>(sp => sp.GetRequiredService<RentVehiclePresenter>());

            services.AddScoped<ReturnVehiclePresenter>();
            services.AddScoped<IReturnVehicleOutputPort>(sp => sp.GetRequiredService<ReturnVehiclePresenter>());

            return services;
        }
    }
}
