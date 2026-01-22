using Microsoft.AspNetCore.Mvc;

namespace _TestWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(DIClass dIClass) : ControllerBase
{
    [HttpGet("singleton")]
    public IActionResult Get()
    {
        dIClass.TCNoKontrol("11");
        return Ok();
    }
}
