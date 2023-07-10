using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace SampleReact.AuthServer.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class AppController : Controller
{
    [HttpGet("~/api/data")]
    public IActionResult FetchData()
    {
        return Ok(new
        {
            FirstName = "John",
            LastName="Doe"
        });
    }
}