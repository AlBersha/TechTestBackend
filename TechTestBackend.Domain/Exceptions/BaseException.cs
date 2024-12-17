using System.Net;

namespace TechTestBackend.Domain.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }

    public BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        HttpStatusCode = statusCode;
    }
}