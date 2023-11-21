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
    private readonly IUserRules _userRules;

    public ExcursionService(IExcursionRepository productRepository, IExcursionRules excursionRules, ITripRules tripRules, IUserRules userRules)
    {
        _excursionRepository = productRepository;
        _excursionRules = excursionRules;
        _tripRules = tripRules;
        _userRules = userRules;
    }

    public Response<ExcursionResponseDto> Add(ExcursionAddRequest excursionAddRequest)
    {
        try
        {
            Excursion excursion = excursionAddRequest;

            _excursionRules.StartDateMustBeSmallerThanEndDate(excursion.StartDate, excursion.EndDate);

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

    public Response<List<ExcursionResponseDto>> GetAllByCostLessThan(decimal costThreshold)
    {
        var excursions = _excursionRepository.GetAll(x=> x.Cost < costThreshold);
        var responses = excursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionResponseDto>> GetAllByCostMoreThan(decimal costThreshold)
    {
        var excursions = _excursionRepository.GetAll(x => x.Cost > costThreshold);
        var responses = excursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionResponseDto>> GetAllByEndDateRange(DateTime min, DateTime max)
    {
        var excursions = _excursionRepository.GetAll(x => x.EndDate <= max && x.EndDate >= min);
        var responses = excursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionResponseDto>> GetAllByLocation(string location)
    {
        var excursions = _excursionRepository.GetAll(x => x.Location == location);
        var responses = excursions.Select(x => (ExcursionResponseDto)x).ToList();

        return new Response<List<ExcursionResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<ExcursionResponseDto>> GetAllByStartDateRange(DateTime min, DateTime max)
    {
        var excursions = _excursionRepository.GetAll(x => x.StartDate <= max && x.StartDate >= min);
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

    public Response<List<ExcursionDetailDto>> GetDetailsByUserId(int userId)
    {
        try
        {
            _userRules.UserIsPresent(userId);
            var details = _excursionRepository.GetExcursionDetailsByUserId(userId);
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
