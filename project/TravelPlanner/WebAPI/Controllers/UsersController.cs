using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly ITripService _tripService;
    private readonly IUserService _userService;

    public UsersController(ITripService tripService, IUserService userService)
    {
        _tripService = tripService;
        _userService = userService;
    }




    //... other methods





    [HttpGet("{id}/trips/details")]
    public IActionResult GetExcursionDetailsByTripId(int id)
    {
        var result = _tripService.GetDetailsByUserId(id);

        return ActionResultInstance(result);
    }
}
