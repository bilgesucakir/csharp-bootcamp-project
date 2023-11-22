using Azure.Core;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstract;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using Service.Rules.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete;

public class ExcursionService : IExcursionService
{

    private readonly IExcursionRepository _excursionRepository;
    private readonly IExcursionRules _excursionRules;
    private readonly ITripRules _tripRules;

    public ExcursionService(IExcursionRepository productRepository, IExcursionRules excursionRules, ITripRules tripRules)
    {
        _excursionRepository = productRepository;
        _excursionRules = excursionRules;
        _tripRules = tripRules;
    }

    public Response<ExcursionResponseDto> Add(ExcursionAddRequest excursionAddRequest)
    {
        try
        {
            Excursion excursion = excursionAddRequest;

            _excursionRules.StartDateMustBeSmallerThanEndDate(excursion.StartDate, excursion.EndDate);

            _excursionRules.ExcursionDateRengeMustBeInTripDateRange(excursion.StartDate, excursion.EndDate, excursion.TripID);

            _tripRules.TripIsPresent(excursion.TripID);

            excursion.Id = new Guid();
            _excursionRepository.Add(excursion);

            ExcursionResponseDto excursionResponse = excursion;

            return new Response<ExcursionResponseDto>
            {
                Data = excursionResponse,
                Message = "Excursion added",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch(BusinessException ex) //will be changes to businessexception later on
        {
            return new Response<ExcursionResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ExcursionResponseDto> Delete(Guid id)
    {
        try
        {
            _excursionRules.ExcursionIsPresent(id);

            var excursion = _excursionRepository.GetById(id);

            _excursionRepository.Delete(excursion);

            ExcursionResponseDto excursionResponse = excursion;

            return new Response<ExcursionResponseDto>
            {
                Data = excursionResponse,
                Message = "Excursion deleted",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ExcursionResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

    }

    public Response<List<ExcursionResponseDto>> GetAll()
    {
        var excursions = _excursionRepository.GetAll();
        var responses = excursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionDetailDto>> GetAllDetails()
    {
        var details = _excursionRepository.GetAllExcursionDetails();

        return new Response<List<ExcursionDetailDto>>()
        {
            Data = details,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionResponseDto>> GetAllFiltered(
        DateTime? minStartDate, 
        DateTime? maxStartDate, 
        DateTime? minEndDate, 
        DateTime? maxEndDate, 
        decimal? minCost, 
        decimal? maxCost, 
        string? location)
    {
        var filteredExcursions = _excursionRepository.GetAll();

        //applying filters based on the provided values
        if (!string.IsNullOrEmpty(location))
        {
            filteredExcursions = filteredExcursions.Where(x => x.Location == location).ToList();
        }

        if (minCost != null && minCost >= 0)
        {
            filteredExcursions = filteredExcursions.Where(x => x.Cost >= minCost).ToList();
        }

        if (maxCost != null && maxCost >= 0)
        {
            filteredExcursions = filteredExcursions.Where(x => x.Cost <= maxCost).ToList();
        }

        if (minStartDate != null)
        {
            filteredExcursions = filteredExcursions.Where(x => x.StartDate >= minStartDate).ToList();
        }

        if (maxStartDate != null)
        {
            filteredExcursions = filteredExcursions.Where(x => x.StartDate <= maxStartDate).ToList();
        }

        if (minEndDate != null)
        {
            filteredExcursions = filteredExcursions.Where(x => x.EndDate >= minEndDate).ToList();
        }

        if (maxEndDate != null)
        {
            filteredExcursions = filteredExcursions.Where(x => x.EndDate <= maxEndDate).ToList();
        }

        var responses = filteredExcursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<ExcursionResponseDto> GetById(Guid id)
    {
        try
        {
            _excursionRules.ExcursionIsPresent(id);

            var excursion = _excursionRepository.GetById(id);

            ExcursionResponseDto excursionResponse = excursion;

            return new Response<ExcursionResponseDto>()
            {
                Data = excursionResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ExcursionResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ExcursionDetailDto> GetDetailById(Guid id)
    {
        try
        {
            _excursionRules.ExcursionIsPresent(id);
            var detail = _excursionRepository.GetExcursionDetail(id);

            return new Response<ExcursionDetailDto>()
            {
                Data = detail,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ExcursionDetailDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<ExcursionDetailDto>> GetDetailsByTripId(Guid tripId)
    {
        try
        {
            _tripRules.TripIsPresent(tripId);
            var details = _excursionRepository.GetExcursionDetailsByTripId(tripId);
            return new Response<List<ExcursionDetailDto>>()
            {
                Data = details,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<List<ExcursionDetailDto>>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<ExcursionResponseDto> Update(ExcursionUpdateRequest excursionUpdateRequest)
    {
        try
        {
            Excursion excursion = excursionUpdateRequest;

            _excursionRules.StartDateMustBeSmallerThanEndDate(excursion.StartDate, excursion.EndDate);

            _excursionRules.ExcursionDateRengeMustBeInTripDateRange(excursion.StartDate, excursion.EndDate, excursion.TripID);

            _tripRules.TripIsPresent(excursion.TripID);

            _excursionRepository.Update(excursion);

            ExcursionResponseDto excursionResponse = excursion;

            return new Response<ExcursionResponseDto>()
            {
                Data = excursionResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<ExcursionResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
