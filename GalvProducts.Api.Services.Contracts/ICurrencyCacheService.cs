using GalvProducts.Api.Common;
using System.Threading.Tasks;

namespace GalvProducts.Api.Services.Contracts
{
    /// <summary>
    /// Cache service for currency rate
    /// </summary>
    public interface ICurrencyCacheService
    {
        /// <summary>
        /// Get currency rate for a given currency code.
        /// </summary>
        /// <param name="currencyEnum">Input currency code</param>
        /// <returns>Currency rate</returns>
        Task<float> GetRate(CurrencyEnum currencyEnum);
    }
}
