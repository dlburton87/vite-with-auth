using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SampleReact.ResourceServer.Controllers;

public class ResourceController : Controller
{
    [Authorize]
    [HttpGet("~/api/resource/{id}")]
    public IActionResult Get(int id)
    {
        return Ok(
            new
            {
                Id = id,
                ResourceName = "Some Protected Resource"
            }
        );
    }
}