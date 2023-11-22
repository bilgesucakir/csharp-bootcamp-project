using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.RequestDto;
using Service.Abstract;

namespace WebAPI.Controllers;

[Route("api/excursions")]
[ApiController]
public class ExcursionsController : BaseController
{
    private readonly IExcursionService _excursionService;

    public ExcursionsController(IExcursionService excursionService)
    {
        _excursionService = excursionService;
    }

    [HttpPost]
    public IActionResult Add([FromBody] ExcursionAddRequest excursionAddRequest)
    {
        var result = _excursionService.Add(excursionAddRequest);

        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] ExcursionUpdateRequest excursionUpdateRequest)
    {
        var result = _excursionService.Update(excursionUpdateRequest);

        return ActionResultInstance(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var result = _excursionService.Delete(id);

        return ActionResultInstance(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _excursionService.GetById(id);

        return ActionResultInstance(result);
    }

    [HttpGet()]
    public IActionResult GetAllOptionalFilters(
        [FromQuery] decimal? minCost,
        [FromQuery] decimal? maxCost,
        [FromQuery] string? location,
        [FromQuery] DateTime? minStartDate,
        [FromQuery] DateTime? maxStartDate,
        [FromQuery] DateTime? minEndDate,
        [FromQuery] DateTime? maxEndDate
    )
    {
        if(minCost == null && maxCost == null && location.IsNullOrEmpty() && 
            minStartDate == null && maxStartDate == null && 
            minEndDate == null && maxEndDate == null)
        {
            var result = _excursionService.GetAll();

            return ActionResultInstance(result);
        }
        else
        {
            var result = _excursionService.GetAllFiltered(
                minStartDate, maxStartDate, minEndDate, maxEndDate, minCost, maxCost, location
                );

            return ActionResultInstance(result);
        }
        
    }

    [HttpGet("details")]
    public IActionResult GetAllDetails()
    {
        var result = _excursionService.GetAllDetails();

        return ActionResultInstance(result);
    }

    [HttpGet("{id}/details")]
    public IActionResult GetDetailById(Guid id)
    {
        var result = _excursionService.GetDetailById(id);

        return ActionResultInstance(result);
    }

}
