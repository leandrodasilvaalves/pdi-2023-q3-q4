using Shared.Contracts.Errors;

namespace Shared.Requests
{
    public class Response<T>
    {
        public Response(T data) => Data = data;
        public Response(IEnumerable<IError> errors) => Errors = errors;
        public Response(IError error) => Errors = new List<IError> { error };

        public bool IsSuccess => !IsFailure;
        public bool IsFailure => Errors?.Any() ?? false;
        public T Data { get; set; }
        public IEnumerable<IError> Errors { get; set; }    
    }
}