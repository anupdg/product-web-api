using Newtonsoft.Json;
using System.Collections.Generic;

namespace GalvProducts.Api.Services.Contracts
{
    /// <summary>
    /// Object place holder for currency rate. Used for https://v6.exchangerate-api.com/v6/
    /// </summary>
    public class CurrencyResult
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("conversion_rates")]
        public Dictionary<string, float> Rates { get; set; }
    }
}
