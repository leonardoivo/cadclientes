using System;
using System.Net;

namespace Tiradentes.CobrancaAtiva.Application.Utils
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode, string message) : base (message)
        {
            StatusCode = statusCode;
        }

        public static CustomException BadRequest(string message) =>
            new CustomException(HttpStatusCode.BadRequest, message);

        public static CustomException EntityNotFound(string message) =>
            new CustomException(HttpStatusCode.NotFound, message);
    }
}
