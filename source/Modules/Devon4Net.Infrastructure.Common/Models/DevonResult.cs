using Devon4Net.Infrastructure.Common.Errors;
using Devon4Net.Infrastructure.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Common.Models;

public class DevonResult<T> : IDevonResult
{
    protected DevonResult() { }

    public DevonResult(T value)
    {
        Data = value;
    }

    public static implicit operator T(DevonResult<T> result) => result.Data;
    public static implicit operator DevonResult<T>(T value) => new DevonResult<T>(value);

    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; init; }

    [JsonIgnore]
    public Type ValueType => typeof(T);

    [JsonInclude]
    public bool IsSuccess => !Errors.Any();

    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<DevonError> Errors { get; protected set; } = [];

    public object GetData()
    {
        return Data;
    }

    public static DevonResult<T> Success(T value)
    {
        return new DevonResult<T>(value);
    }

    public static DevonResult<T> Error(DevonError devonError)
    {
        return new DevonResult<T>() { Errors = [devonError] };
    }

    public static DevonResult<T> Error(IEnumerable<DevonError> devonErrors = null)
    {
        return new DevonResult<T>()
        {
            Errors = devonErrors
        };
    }

    public void AddError(DevonError error)
    {
        IEnumerable<DevonError> errors;
        if (Errors != null)
        {
            errors = Errors.Append(error);
        }
        else
        {
            errors = [];
        }

        Errors = errors;
    }

    public IEnumerable<DevonError> GetErrors()
    {
        return Errors;
    }

    public bool ReturnedError(DevonError errorCode)
    {
        return Errors?.Any((DevonError myError) => myError.Code == $"{errorCode}") ?? false;
    }

    public bool ReturnedError(IEnumerable<DevonError> errorCodes)
    {
        return errorCodes.Any((DevonError errorCode) => Errors?.Any((DevonError myError) => myError.Code == $"{errorCode}") ?? false);
    }

    public IActionResult BuildResult(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        if (IsSuccess)
        {
            T dataAsT = Data;
            if(dataAsT == null)
            {
                return new NoContentResult();
            }
            
            return new OkObjectResult(dataAsT)
            {
                StatusCode = (int)httpStatusCode,
            };

        }
        return new ObjectResult(Errors)
        {
            StatusCode = Errors.ToHigherHttpStatusCode(),
        };
    }
}