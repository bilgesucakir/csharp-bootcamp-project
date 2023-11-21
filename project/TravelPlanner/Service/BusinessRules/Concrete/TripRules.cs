using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Service.Rules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Concrete;

public class TripRules : ITripRules
{
    private readonly ITripRepository _tripRepository;

    public TripRules(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public void TripIsPresent(Guid id)
    {
        var trip = _tripRepository.GetById(id);
        if (trip == null)
        {
            throw new BusinessException($"No trip found with id: {id}");
        }
    }

    public void StartDateMustBeSmallerThanEndDate(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new BusinessException("Trip start date cannot be bigger than or equal to end date");
        }
    }
}
