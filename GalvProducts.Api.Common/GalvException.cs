using Microsoft.AspNetCore.Http;
using System;

namespace GalvProducts.Api.Common
{
    /// <summary>
    /// Custom exception used to handle API response in case of this error type
    /// </summary>
    public class GalvException : Exception
    {
        public int StatusCode { get; set; }
        public GalvException()
        {
        }
        public GalvException(string message, int statusCode = StatusCodes.Status500InternalServerError) : base(String.Format("Error : {0}", message))
        {
            StatusCode = statusCode;
        }
    }
}
