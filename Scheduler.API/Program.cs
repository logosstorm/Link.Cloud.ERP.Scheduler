using Quartz;
using Scheduler.API.Jobs.HelloJob;
using Scheduler.API.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(ELKLoggingConfiguration.ConfigureLogger);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    // Use a Scoped container to create jobs. I'll touch on this later
    q.UseMicrosoftDependencyInjectionJobFactory();

    q.AddHelloWordJob();
});

// Add the Quartz.NET hosted service

builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();