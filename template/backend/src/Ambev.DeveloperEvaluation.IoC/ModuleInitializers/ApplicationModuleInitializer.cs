using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        
        // Sales command handlers
        builder.Services.AddScoped<CreateSaleCommandHandler>();
        builder.Services.AddScoped<UpdateSaleCommandHandler>();
        builder.Services.AddScoped<CancelSaleCommandHandler>();
        builder.Services.AddScoped<CancelSaleItemCommandHandler>();
        
        // Sales query handlers
        builder.Services.AddScoped<GetSaleByIdHandler>();
        builder.Services.AddScoped<ListSalesHandler>();
    }
}
