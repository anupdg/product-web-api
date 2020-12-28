using GalvProducts.Api.Common;
using GalvProducts.Api.Services.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalvProducts.Api.Services
{
    /// <summary>
    /// Read currency rate from external API, this code works with currency rates taken from https://v6.exchangerate-api.com/v6/
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly CurrencyConfigOptions _options;
        public CurrencyService(HttpClient httpClient, IOptions<CurrencyConfigOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        /// <summary>
        /// Get rates for a day
        /// </summary>
        /// <returns>Returns all available currency rates</returns>
        public async Task<Dictionary<string, float>> GetRates()
        {
            var responseString = await _httpClient.GetStringAsync(_options.Url);
            var rates = JsonConvert.DeserializeObject<CurrencyResult>(responseString);
            return rates.Rates;
        }
    }
}
