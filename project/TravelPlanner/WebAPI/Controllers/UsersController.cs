using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.RequestDto;
using Service.Abstract;
using Service.Concrete;

namespace WebAPI.Controllers;

[Route("api/users")]
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

    [HttpPost]
    public IActionResult Add([FromBody] UserAddRequest userAddRequest)
    {
        var result = _userService.Add(userAddRequest);

        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UserUpdateRequest userUpdateRequest)
    {
        var result = _userService.Update(userUpdateRequest);

        return ActionResultInstance(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _userService.Delete(id);

        return ActionResultInstance(result);
    }

    [HttpGet()]
    public IActionResult GetAllOptionalFilters(
        [FromQuery] string? name,
        [FromQuery] string? surname
    )
    {
        if (name.IsNullOrEmpty() && surname.IsNullOrEmpty())
        {
            var result = _userService.GetAll();

            return ActionResultInstance(result);
        }
        else
        {
            var result = _userService.GetAllFiltered(name, surname);

            return ActionResultInstance(result);
        }

    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _userService.GetById(id);

        return ActionResultInstance(result);
    }

    [HttpGet("{id}/trips")]
    public IActionResult GetTripssByUserId(int id)
    {
        var result = _tripService.GetByUserId(id);

        return ActionResultInstance(result);
    }

    [HttpGet("{id}/trips/details")]
    public IActionResult GetTripDetailsByUserId(int id)
    {
        var result = _tripService.GetDetailsByUserId(id);

        return ActionResultInstance(result);
    }
}
