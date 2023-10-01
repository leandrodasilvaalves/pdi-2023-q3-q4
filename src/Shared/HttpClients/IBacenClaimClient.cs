using Refit;
using Shared.Entities;
using Shared.Requests;

namespace Shared.HttpClients
{
    public interface IBacenClaimClient
    {
        [Post("/api/pix/claims")]
        Task<IApiResponse<Response<Claim>>> RegisterAsync(RegisterClaimRequest claim);

        [Patch("/{id}/confirm")]
        Task<IApiResponse<Response<Claim>>> ConfirmAsync(string id);
    }
}