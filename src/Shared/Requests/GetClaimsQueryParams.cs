using Refit;

namespace Shared.Requests
{
    public class GetClaimsQueryParams
    {
        public GetClaimsQueryParams() { }

        public GetClaimsQueryParams(DateTime starDate, DateTime endDate)
        {
            StartDate = starDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            EndDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        [AliasAs("startDate")]
        public string StartDate { get; set; }
        
        [AliasAs("endDate")]
        public string EndDate { get; set; }
    }
}