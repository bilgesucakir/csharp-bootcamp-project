using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.RequestDto;
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

    [HttpPost]
    public IActionResult Add([FromBody] TripAddRequest tripAddRequest)
    {
        var result = _tripService.Add(tripAddRequest);

        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] TripUpdateRequest tripUpdateRequest)
    {
        var result = _tripService.Update(tripUpdateRequest);

        return ActionResultInstance(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var result = _tripService.Delete(id);

        return ActionResultInstance(result);
    }

    [HttpGet()]
    public IActionResult GetAllOptionalFilters(
        [FromQuery] decimal? minBudget,
        [FromQuery] decimal? maxBudget,
        [FromQuery] DateTime? minStartDate,
        [FromQuery] DateTime? maxStartDate,
        [FromQuery] DateTime? minEndDate,
        [FromQuery] DateTime? maxEndDate
    )
    {
        if (minBudget == null && maxBudget == null &&
            minStartDate == null && maxStartDate == null &&
            minEndDate == null && maxEndDate == null)
        {
            var result = _tripService.GetAll();

            return ActionResultInstance(result);
        }
        else
        {
            var result = _tripService.GetAllFiltered(
                minStartDate, maxStartDate, minEndDate, maxEndDate, minBudget, maxBudget
            );

            return ActionResultInstance(result);
        }

    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _tripService.GetById(id);

        return ActionResultInstance(result);
    }

    [HttpGet("details")]
    public IActionResult GetAllDetails()
    {
        var result = _tripService.GetAllDetails();

        return ActionResultInstance(result);
    }

    [HttpGet("{id}/details")]
    public IActionResult GetDetailById(Guid id)
    {
        var result = _tripService.GetDetailById(id);

        return ActionResultInstance(result);
    }

    [HttpGet("{id}/excursions/details")]
    public IActionResult GetExcursionDetailsByTripId(Guid id)
    {
        var result = _excursionService.GetDetailsByTripId(id);

        return ActionResultInstance(result);
    }
}
