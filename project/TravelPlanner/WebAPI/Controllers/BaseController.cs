using Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    public IActionResult ActionResultInstance<T>(Response<T> response)
    {
        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.OK:
                return Ok(response);
            case System.Net.HttpStatusCode.Created:
                return BadRequest(response);
            case System.Net.HttpStatusCode.BadGateway:
                return BadRequest(response);
            default: 
                return NotFound();
        }
    }
}
