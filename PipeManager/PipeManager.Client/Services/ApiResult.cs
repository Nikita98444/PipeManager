using PipeManager.Client.Contracts;
using System.Net;

namespace PipeManager.Client.Services
{
    public sealed record ApiResult<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public ClientErrorResponse? Error { get; init; }
        public HttpStatusCode? StatusCode { get; init; }

        public static ApiResult<T> Ok(T data) => new()
        {
            IsSuccess = true,
            Data = data
        };

        public static ApiResult<T> Fail(ClientErrorResponse error,
                                        HttpStatusCode statusCode) => new()
                                        {
                                            IsSuccess = false,
                                            Error = error,
                                            StatusCode = statusCode
                                        };
    }
}
