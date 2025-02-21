using Microsoft.EntityFrameworkCore;
using Potter.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PotterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient(); 

var corsPolicy = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Permitir o frontend Vue.js
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();
