using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using AssetRouter;
using AssetRouter.Core.Interfaces;
using AssetRouter.Infrastructure.Strategies;
using AssetRouter.Infrastructure.Repositories;
using AssetRouter.Infrastructure.Data;
using AssetRouter.Application.Services;
using AssetRouter.Infrastructure.DataSources;
using AssetRouter.Infrastructure.Persistence;

Batteries_V2.Init();

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=local_allocation.db"));

builder.Services.AddScoped<IAllocationRepository, AllocationRepository>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IDbPersistence, IndexedDbPersistence>();

builder.Services.AddScoped<IStockDataSource, HttpStockDAtaSource>();
builder.Services.AddScoped<StockScreenerService>();

builder.Services.AddScoped<IAllocationPreset, DefaultPreset>();
builder.Services.AddScoped<IAllocationPreset, ConservativePreset>();
builder.Services.AddScoped<IAllocationPreset, AggressivePreset>();

builder.Services.AddScoped<PortfolioManagerService>();

builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

var host = builder.Build();

using (var scope = host.Services.CreateScope()) {
    var persistence = scope.ServiceProvider.GetRequiredService<IDbPersistence>();
    await persistence.RestoreAsync();

    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

await host.RunAsync();
