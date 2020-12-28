using GalvProducts.Api.Common;
using GalvProducts.Api.Services.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalvProducts.Api.Services
{
    /// <summary>
    /// Cache service for currency rate. Here we are using memory cache for caching rate for a day
    /// </summary>
    public class CurrencyCacheService : ICurrencyCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CurrencyCacheService> _logger;
        private readonly ICurrencyService _currencyService;
        public CurrencyCacheService(IMemoryCache memoryCache, ILogger<CurrencyCacheService> logger, ICurrencyService currencyService)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _currencyService = currencyService;
        }
        
        /// <summary>
        /// Get currency rate for a given currency. It tries to read rate from memory for a day. If not found it reads currency from API
        /// </summary>
        /// <param name="currencyEnum">Input currency code</param>
        /// <returns>Currency rate</returns>
        public async Task<float> GetRate(CurrencyEnum currencyEnum)
        {
            var currency = currencyEnum.ToString();
            var key = $"{DateTime.Today.ToShortDateString()}";
            Dictionary<string, float> rates;
            _memoryCache.TryGetValue(key, out rates);
            if (rates == null)
            {
                var rateResponse = await _currencyService.GetRates();
                _memoryCache.Set(key, rateResponse);
                rates = rateResponse;
            }

            if (rates.ContainsKey(currency))
            {
                return rates[currency];
            }
            else {
                _logger.LogError($"Not able to get currency rate. Currency code {currency} not found");
                throw new GalvException("Not able to get currency rate. Currency code not found");
            }
        }
    }
}
