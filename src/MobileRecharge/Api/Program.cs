using Microsoft.EntityFrameworkCore;
using MobileRecharge.Core;
using MobileRecharge.Core.Queries;
using MobileRecharge.Core.Services;
using OnlineWallet.Infrastructure;
using OnlineWallet.Infrastructure.Data;
using OnlineWallet.Infrastructure.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.ConfigureDbContext(configuration);

builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();
builder.Services.AddValidators();

builder.Services.AddTransient<IDebitAccountService, DebitAccountService>();

var accountBalanceServiceBaseUrl = new Uri(configuration["AccountBalanceServiceBaseUrl"]);

builder.Services.AddHttpClient("AccountBalance", httpClient =>
{
    httpClient.BaseAddress = accountBalanceServiceBaseUrl;
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTopUpOptionsQuery).Assembly));

builder.Services.Configure<ValidationConfiguration>(builder.Configuration.GetSection(ValidationConfiguration.TopUpValidationRules));

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<OnlineWalletDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();
