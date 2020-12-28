using Newtonsoft.Json;

namespace GalvProducts.Api.Common
{
    /// <summary>
    /// Custom API response payload in case of error
    /// </summary>
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
