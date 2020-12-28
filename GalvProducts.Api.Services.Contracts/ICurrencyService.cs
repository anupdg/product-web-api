using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalvProducts.Api.Services.Contracts
{
    /// <summary>
    /// Read currency rate from external API, 
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Get rates for a day
        /// </summary>
        /// <returns>Currency rates</returns>
        Task<Dictionary<string, float>> GetRates();
    }
}
