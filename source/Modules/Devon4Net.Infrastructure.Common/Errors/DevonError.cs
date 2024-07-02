using System.Net;

namespace Devon4Net.Infrastructure.Common.Errors;

public class DevonError
{
    public string Code { get; init; }

    public string Message { get; init; }

    public HttpStatusCode HttpStatus { get; private set; }

    public DevonError(string code, string message, HttpStatusCode? httpStatus = null)
    {
        Code = code;
        Message = message;
        HttpStatus = httpStatus ?? HttpStatusCode.InternalServerError;
    }

}