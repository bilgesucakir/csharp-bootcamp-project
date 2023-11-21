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

    public ExcursionRules(IExcursionRepository excursionRepository)
    {
        _excursionRepository = excursionRepository;
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
