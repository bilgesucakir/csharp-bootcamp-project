using Core.Shared;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract;

public interface IExcursionService
{
    Response<ExcursionResponseDto> Add(ExcursionAddRequest excursionAddRequest);
    Response<ExcursionResponseDto> Update(ExcursionUpdateRequest excursionUpdateRequest);
    Response<ExcursionResponseDto> Delete(Guid id);

    Response<ExcursionResponseDto> GetById(Guid id);
    Response<List<ExcursionResponseDto>> GetAll();

    Response<List<ExcursionResponseDto>> GetAllFiltered(
        DateTime? minStartDate,
        DateTime? maxStartDate,
        DateTime? minEndDate,
        DateTime? maxEndDate,
        decimal? minCost,
        decimal? maxCost,
        string? location
    );

    Response<List<ExcursionResponseDto>> GetByTripId(Guid tripId);

    Response<ExcursionDetailDto> GetDetailById(Guid id);
    Response<List<ExcursionDetailDto>> GetAllDetails();
    Response<List<ExcursionDetailDto>> GetDetailsByTripId(Guid tripId);
}
