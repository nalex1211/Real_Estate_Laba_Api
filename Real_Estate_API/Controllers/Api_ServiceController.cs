using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate_API.Helpers;

namespace Real_Estate_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Api_ServiceController : ControllerBase
{
    private readonly Api_Service _service;

    public Api_ServiceController(Api_Service service)
    {
        _service = service;
    }
    [HttpGet("city")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetPropertyList(string city)
    {
        var properties = await _service.GetPropertyData(city);
        return Ok(properties);
    }
}
