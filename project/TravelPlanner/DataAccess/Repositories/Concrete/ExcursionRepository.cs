using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Models.Dtos.ResponseDto;
using Models.Entities;

namespace DataAccess.Repositories.Concrete;

public class ExcursionRepository : EfRepositoryBase<BaseDbContext, Excursion, Guid>, IExcursionRepository
{
    public ExcursionRepository(BaseDbContext context) : base(context)
    {
    }

    public List<ExcursionDetailDto> GetAllExcursionDetails()
    {
        var details = Context.Excursions.Join(Context.Trips,
            e => e.TripID,
            t => t.Id,
            (excursion, trip) => new ExcursionDetailDto
            {
                Id = excursion.Id,
                Name = excursion.Name,
                StartDate = excursion.StartDate,
                EndDate = excursion.EndDate,
                Location = excursion.Location,
                Description = excursion.Description,
                Cost = excursion.Cost,
                TripTitle = trip.Title,
                TripStartDate = trip.StartDate,
                TripEndDate = trip.EndDate,
            }
            ).ToList();


        return details;
    }

    public List<ExcursionDetailDto> GetExcursionDetailsByTripId(Guid TripId)
    {
            var details = Context.Excursions.Where(x => x.TripID == TripId).Join(
                Context.Trips,
                e => e.TripID,
                t => t.Id,
                (e, t) => new ExcursionDetailDto
                {
                    TripTitle = t.Title,
                    TripStartDate = t.StartDate,
                    TripEndDate = t.EndDate,
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Location = e.Location,
                    Description = e.Description,
                    Cost = e.Cost
                }
                ).ToList();
            return details;
    }

    public ExcursionDetailDto GetExcursionDetail(Guid id)
    {
        var detail = Context.Excursions.Join(
            Context.Trips,
            e => e.TripID,
            t => t.Id,
            (e, t) => new ExcursionDetailDto
            {
                TripTitle = t.Title,
                TripStartDate = t.StartDate,
                TripEndDate = t.EndDate,
                Id = e.Id,
                Name = e.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Description = e.Description,
                Cost = e.Cost
            }
            ).SingleOrDefault(x => x.Id == id);

        return detail;
    }
}
