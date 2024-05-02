using AccountBalance.Core;
using AccountBalance.Core.Commands;
using AccountBalance.Core.Validations.Account;
using OnlineWallet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.ConfigureDbContext(configuration);
builder.Services.AddCommandHandlers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DebitAccountCommand).Assembly));

builder.Services.AddTransient<IAccountValidationService, AccountValidationService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
