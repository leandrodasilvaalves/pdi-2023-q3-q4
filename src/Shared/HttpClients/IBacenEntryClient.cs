using Refit;
using Shared.Entities;
using Shared.Requests;

namespace Shared.HttpClients
{
    public interface IBacenEntryClient
    {
        [Post("/api/pix/entries")]
        Task<IApiResponse<Response<Entry>>> RegisterAsync(CreateEntryRequest request);
    }
}