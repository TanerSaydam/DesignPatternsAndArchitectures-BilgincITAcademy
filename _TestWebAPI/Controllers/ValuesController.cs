using Microsoft.AspNetCore.Mvc;

namespace _TestWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(
    DIClass dIClass,
    [FromKeyedServices("sms")] INotification notification) : ControllerBase
{
    [HttpGet("singleton")]
    public IActionResult Get()
    {
        dIClass.TCNoKontrol("11");
        return Ok();
    }

    [HttpGet("factory")]
    [Validation]
    public IActionResult NotificationFactory()
    {
        notification.Send("Hello world");
        return Ok();
    }
}
