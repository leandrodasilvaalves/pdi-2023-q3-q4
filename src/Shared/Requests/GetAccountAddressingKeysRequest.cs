using Microsoft.AspNetCore.Mvc;

namespace Shared.Requests
{
    public class GetAccountAddressingKeysRequest
    {
        [FromRoute] public string Ispb { get; set; }
        [FromRoute] public string Branch { get; set; }
        [FromRoute] public string Account { get; set; }
    }
}