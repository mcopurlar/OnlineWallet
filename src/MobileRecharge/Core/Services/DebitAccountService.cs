using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MobileRecharge.Core.Services;

public class DebitAccountService : IDebitAccountService
{
    private readonly ILogger<DebitAccountService> _logger;
    private readonly HttpClient _httpClient;
    private readonly decimal _topUpDebitCost;

    public DebitAccountService(IHttpClientFactory httpClientFactory, ILogger<DebitAccountService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("AccountBalance");
        _topUpDebitCost = decimal.Parse(configuration["TopUpDebitCost"]);
    }

    public async Task<DebitAccountResponse> DebitAccount(Guid userId, decimal amount)
    {
        amount += _topUpDebitCost;

        var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/accounts/debit", new DebitAccountRequestModel
        {
            Amount = amount,
            UserId = userId
        });

        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogError("Error occured while charging the account : {responseContent}");

            var problemDetailsList = JsonConvert.DeserializeObject<IList<ProblemDetails>>(responseContent);
            var errorMessages = problemDetailsList.Where(pd => pd.Status == StatusCodes.Status400BadRequest).Select(pd => pd.Title).ToList();

            var debitAccountResponse = new DebitAccountResponse
            {
                IsSuccessful = false,
                ErrorMessages = errorMessages
            };

            return debitAccountResponse;
        }

        return new DebitAccountResponse
        {
            IsSuccessful = true
        };
    }
}