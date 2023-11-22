using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : BaseController
{
    private readonly ITripService _tripService;
    private readonly IExcursionService _excursionService;

    public TripsController(ITripService tripService, IExcursionService excursionService)
    {
        _tripService = tripService;
        _excursionService = excursionService;
    }



    //... other methods





    [HttpGet("{id}/excursions/details")]
    public IActionResult GetExcursionDetailsByTripId(Guid id)
    {
        var result = _excursionService.GetDetailsByTripId(id);

        return ActionResultInstance(result);
    }
}
