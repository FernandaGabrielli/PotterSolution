using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    // Configuring Hangfire to use in-memory storage
    services.AddHangfire(config => config.UseMemoryStorage());
    services.AddHangfireServer();

    // Adding HttpClient and the DataUpdater to Dependency Injection (DI)
    services.AddHttpClient<DataUpdater>();
    services.AddTransient<DataUpdater>();

    // Registering the Worker as a hosted service
    services.AddHostedService<Worker>();
});

var app = builder.Build();

// Configuring the Recurring Job
using (var scope = app.Services.CreateScope())
{
    var jobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    // Adding or updating the recurring job to update characters every hour
    jobManager.AddOrUpdate<DataUpdater>(
        "update-characters", 
        x => x.UpdateCharacters(), 
        Cron.Hourly);
}

app.Run();
