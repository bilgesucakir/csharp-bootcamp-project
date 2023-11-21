using Core.Shared;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract;

public interface ITripService
{
    Response<TripResponseDto> Add(TripAddRequest tripAddRequest);
    Response<TripResponseDto> Update(TripUpdateRequest tripUpdateRequest);
    Response<TripResponseDto> Delete(Guid id);

    Response<TripResponseDto> GetById(Guid id);
    Response<List<TripResponseDto>> GetAll();

    Response<List<TripResponseDto>> GetAllByStartDateRange(DateTime min, DateTime maz);
    Response<List<TripResponseDto>> GetAllByEndDateRange(DateTime min, DateTime max);
    Response<List<TripResponseDto>> GetAllByBudgetLessThan(decimal budgetThreshold);
    Response<List<TripResponseDto>> GetAllByBudgetMoreThan(decimal budgetThreshold);


    Response<TripDetailDto> GetDetailById(Guid id);
    Response<List<TripDetailDto>> GetAllDetails();
    Response<List<TripDetailDto>> GetDetailsByUserId(Guid userId);

}
