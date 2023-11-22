using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Microsoft.Identity.Client;
using Service.Rules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Concrete;

public class ExcursionRules : IExcursionRules
{

    private readonly IExcursionRepository _excursionRepository;
    private readonly ITripRepository _tripRepository;

    public ExcursionRules(IExcursionRepository excursionRepository, ITripRepository tripRepository)
    {
        _excursionRepository = excursionRepository;
        _tripRepository = tripRepository;
    }

    public void ExcursionDateRengeMustBeInTripDateRange(DateTime excursionStartDate, DateTime excursionEndDate, Guid tripId) //might thow error with repo, check 
    {

        var tripStartDate = _tripRepository.GetById(tripId).StartDate;
        var tripEndDate = _tripRepository.GetById(tripId).EndDate;

        if(tripStartDate > excursionStartDate || tripEndDate < excursionEndDate) 
        {
            throw new BusinessException($"Excursion date range cannot be outside of trip date range");
        }
    }

    public void ExcursionIsPresent(Guid id)
    {
        var excursion = _excursionRepository.GetById(id);
        if(excursion == null)
        {
            throw new BusinessException($"No excursion found with id: {id}");
        }
    }

    public void StartDateMustBeSmallerThanEndDate(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new BusinessException("Excursion start date cannot be bigger than or equal to end date");
        }
    }
}
