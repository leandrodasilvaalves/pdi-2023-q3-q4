using Refit;
using Shared.Entities;
using Shared.Requests;

namespace Shared.Extensions
{
    public static class RefitExtensions
    {
        public static bool IsFailure<T>(this IApiResponse<Response<T>> self)
        {
            return !self.IsSuccessStatusCode && self.Error is not null;
        }

        public static async Task<Response<T>> ToFailureAsync<T>(this IApiResponse<Response<T>> self)
        {
            return await self.Error.GetContentAsAsync<Response<T>>();
        }

        public static string GetId<T>(this IApiResponse<Response<T>> self) where T : EntityBase
        {
            return self.Content.Data.Id;
        }
    }
}