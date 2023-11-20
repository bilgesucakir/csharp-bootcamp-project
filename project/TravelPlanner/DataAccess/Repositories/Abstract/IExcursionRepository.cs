using Core.Persistence.Repositories;
using Models.Dtos.ResponseDto;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract;

public interface IExcursionRepository : IEntityRepository<Excursion, Guid>
{
    List<ExcursionDetailDto> GetAllExcursionDetails();

    List<ExcursionDetailDto> GetExcursionDetailsByTripId(Guid TripId);

    List<ExcursionDetailDto> GetExcursionDetailsByUserId(int userId); //can be removed

    ExcursionDetailDto GetExcursionDetail(Guid id);
}