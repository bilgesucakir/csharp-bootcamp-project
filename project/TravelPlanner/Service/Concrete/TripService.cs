using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using Service.Rules.Abstract;
using Service.Rules.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly ITripRules _tripRules;
    private readonly IUserRules _userRules;

    public TripService(ITripRepository tripRepository, ITripRules tripRules, IUserRules userRules)
    {
        _tripRepository = tripRepository;
        _tripRules = tripRules;
        _userRules = userRules;
    }

    public Response<TripResponseDto> Add(TripAddRequest tripAddRequest)
    {
        try
        {
            Trip trip = tripAddRequest;

            _tripRules.StartDateMustBeSmallerThanEndDate(trip.StartDate, trip.EndDate);

            _userRules.UserIsPresent(trip.UserID);

            trip.Id = new Guid();
            _tripRepository.Add(trip);

            TripResponseDto tripResponse = trip;

            return new Response<TripResponseDto>
            {
                Data = tripResponse,
                Message = "Trip added",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<TripResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<TripResponseDto> Delete(Guid id)
    {
        try
        {
            _tripRules.TripIsPresent(id);

            var trip = _tripRepository.GetById(id);

            _tripRepository.Delete(trip);

            TripResponseDto tripResponse = trip;

            return new Response<TripResponseDto>
            {
                Data = tripResponse,
                Message = "Trip deleted",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<TripResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<TripResponseDto>> GetAll()
    {
        var trips = _tripRepository.GetAll();
        var responses = trips.Select(x => (TripResponseDto)x).ToList();

        return new Response<List<TripResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<TripDetailDto>> GetAllDetails()
    {
        var details = _tripRepository.GetAllTripDetails();

        return new Response<List<TripDetailDto>>()
        {
            Data = details,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<TripResponseDto>> GetAllFiltered(
        DateTime? minStartDate, 
        DateTime? maxStartDate, 
        DateTime? minEndDate, 
        DateTime? maxEndDate, 
        decimal? minBudget, 
        decimal? maxBudget
    )
    {
        var filteredTrips = _tripRepository.GetAll();

        //applying filters based on the provided values
        if (minBudget != null && minBudget >= 0)
        {
            filteredTrips = filteredTrips.Where(x => x.Budget >= minBudget).ToList();
        }

        if (maxBudget != null && maxBudget >= 0)
        {
            filteredTrips = filteredTrips.Where(x => x.Budget <= maxBudget).ToList();
        }

        if (minStartDate != null)
        {
            filteredTrips = filteredTrips.Where(x => x.StartDate >= minStartDate).ToList();
        }

        if (maxStartDate != null)
        {
            filteredTrips = filteredTrips.Where(x => x.StartDate <= maxStartDate).ToList();
        }

        if (minEndDate != null)
        {
            filteredTrips = filteredTrips.Where(x => x.EndDate >= minEndDate).ToList();
        }

        if (maxEndDate != null)
        {
            filteredTrips = filteredTrips.Where(x => x.EndDate <= maxEndDate).ToList();
        }

        var responses = filteredTrips.Select(x => (TripResponseDto)x).ToList();

        return new Response<List<TripResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<TripResponseDto> GetById(Guid id)
    {
        try
        {
            _tripRules.TripIsPresent(id);

            var trip = _tripRepository.GetById(id);

            TripResponseDto tripResponse = trip;

            return new Response<TripResponseDto>()
            {
                Data = tripResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<TripResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<TripDetailDto> GetDetailById(Guid id)
    {
        try
        {
            _tripRules.TripIsPresent(id);
            var detail = _tripRepository.GetTripDetail(id);

            return new Response<TripDetailDto>()
            {
                Data = detail,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<TripDetailDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<TripDetailDto>> GetDetailsByUserId(int userId)
    {
        try
        {
            _userRules.UserIsPresent(userId);
            var details = _tripRepository.GetTripDetailByUserId(userId);
            return new Response<List<TripDetailDto>>()
            {
                Data = details,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<List<TripDetailDto>>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<TripResponseDto> Update(TripUpdateRequest tripUpdateRequest)
    {
        try
        {
            Trip trip = tripUpdateRequest;

            _tripRules.StartDateMustBeSmallerThanEndDate(trip.StartDate, trip.EndDate);

            _userRules.UserIsPresent(trip.UserID);

            _tripRepository.Update(trip);

            TripResponseDto tripResponse = trip;

            return new Response<TripResponseDto>()
            {
                Data = tripResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<TripResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
