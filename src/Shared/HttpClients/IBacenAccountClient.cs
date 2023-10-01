using Refit;
using Shared.Entities;
using Shared.Requests;

namespace Shared.HttpClients
{
    public interface IBacenAccountClient
    {
        [Post("/api/account")]
        Task<IApiResponse<Response<Account>>> RegisterAsync(CreateAccountRequest request);
    }
}