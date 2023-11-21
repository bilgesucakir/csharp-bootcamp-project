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

    Response<List<ExcursionResponseDto>> GetAllByStartDateRange(DateTime min, DateTime max);
    Response<List<ExcursionResponseDto>> GetAllByEndDateRange(DateTime min, DateTime max);
    Response<List<ExcursionResponseDto>> GetAllByCostLessThan(decimal costThreshold);
    Response<List<ExcursionResponseDto>> GetAllByCostMoreThan(decimal costThreshold);
    Response<List<ExcursionResponseDto>> GetAllByLocation(string location);

    Response<ExcursionDetailDto> GetDetailById(Guid id);
    Response<List<ExcursionDetailDto>> GetAllDetails();
    Response<List<ExcursionDetailDto>> GetDetailsByUserId(int userId); //extra method
    Response<List<ExcursionDetailDto>> GetDetailsByTripId(Guid tripId);
}
