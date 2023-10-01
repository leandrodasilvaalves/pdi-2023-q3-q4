using Shared.Contracts.Errors;

namespace Shared.Requests
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data) => Data = data;
        public Response(IEnumerable<Error> errors) => Errors = errors;
        public Response(Error error) => Errors = new List<Error> { error };

        public bool IsSuccess => !IsFailure;
        public bool IsFailure => Errors?.Any() ?? false;
        public T Data { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }
}