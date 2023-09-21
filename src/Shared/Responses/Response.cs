using Shared.Contracts;

namespace Shared.Requests
{
    public class Response<T>
    {
        public Response(T data) => Data = data;
        public Response(IEnumerable<Error> errors) => Errors = errors;

        public bool IsSuccess => !IsFailure;
        public bool IsFailure => Errors?.Any() ?? false;
        public T Data { get; set; }
        public IEnumerable<Error> Errors { get; set; }    
    }
}