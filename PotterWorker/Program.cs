using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    // Configuração do Hangfire usando armazenamento em memória
    services.AddHangfire(config => config.UseMemoryStorage());
    services.AddHangfireServer();

    // Adicionando HttpClient e o DataUpdater ao DI
    services.AddHttpClient<DataUpdater>();
    services.AddTransient<DataUpdater>();

    // Registrando o Worker
    services.AddHostedService<Worker>();
});

var app = builder.Build();

// Configuração do Job Recorrente
using (var scope = app.Services.CreateScope())
{
    var jobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    jobManager.AddOrUpdate<DataUpdater>(
        "update-characters", 
        x => x.UpdateCharacters(), 
        Cron.Hourly);
}

app.Run();
