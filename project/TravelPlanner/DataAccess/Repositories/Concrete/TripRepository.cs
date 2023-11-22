using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Models.Dtos.ResponseDto;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete;

public class TripRepository : EfRepositoryBase<BaseDbContext, Trip, Guid>, ITripRepository
{
    public TripRepository(BaseDbContext context) : base(context)
    {
    }

    public List<TripDetailDto> GetAllTripDetails()
    {
        var details = Context.Trips.Join(Context.Users,
            t => t.UserID,
            u => u.Id,
            (trip, user) => new TripDetailDto
            {
                Id = trip.Id,
                Title = trip.Title,
                Description = trip.Description,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Budget = trip.Budget,
                UserEmail = user.Email,
                UserName = user.Name,
                UserSurname = user.Surname

            }
            ).ToList();


        return details;
    }

    public TripDetailDto GetTripDetail(Guid id)
    {
        var detail = Context.Trips.Join(
            Context.Users,
            t => t.UserID,
            u => u.Id,
            (t, u) => new TripDetailDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Budget = t.Budget,
                UserEmail = u.Email,
                UserName = u.Name,
                UserSurname = u.Surname
            }
            ).SingleOrDefault(x => x.Id == id);

        return detail;
    }

    public List<TripDetailDto> GetTripDetailByUserId(int UserId)
    {
        var details = Context.Trips.Where(x => x.UserID == UserId).Join(
                Context.Users,
                t => t.UserID,
                u => u.Id,
                (t, u) => new TripDetailDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Budget = t.Budget,
                    UserEmail = u.Email,
                    UserName = u.Name,
                    UserSurname = u.Surname
                }
                ).ToList();
        return details;
    }
}
