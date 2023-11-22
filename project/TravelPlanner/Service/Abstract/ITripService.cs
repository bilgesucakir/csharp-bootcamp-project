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

    Response<List<TripResponseDto>> GetAllFiltered(
        DateTime? minStartDate,
        DateTime? maxStartDate,
        DateTime? minEndDate,
        DateTime? maxEndDate,
        decimal? minBudget,
        decimal? maxBudget
    );

    Response<List<TripResponseDto>> GetByUserId(int userId);

    Response<TripDetailDto> GetDetailById(Guid id);
    Response<List<TripDetailDto>> GetAllDetails();
    Response<List<TripDetailDto>> GetDetailsByUserId(int userId);

}
