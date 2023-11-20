using Core.Persistence.EntityBaseModel;
using Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Trip: Entity<Guid>
{
    //guid
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }

    //user-trip
    public int UserID { get; set; } //foreign key
    public User User { get; set; }

    //trip-event
    public List<Activity> Activities { get; set;}

    //operators
    public static implicit operator Trip(TripAddRequest tripAddRequest)
        => new Trip
        {
            Title = tripAddRequest.Title,
            Description = tripAddRequest.Description,
            StartDate = tripAddRequest.StartDate,
            EndDate = tripAddRequest.EndDate,
            Budget = tripAddRequest.Budget,
            UserID = tripAddRequest.UserID
        };

    public static implicit operator Trip(TripUpdateRequest tripUpdateRequest)
        => new Trip
        {
            Id = tripUpdateRequest.Id,
            Title = tripUpdateRequest.Title,
            Description = tripUpdateRequest.Description,
            StartDate = tripUpdateRequest.StartDate,
            EndDate = tripUpdateRequest.EndDate,
            Budget = tripUpdateRequest.Budget,
            UserID = tripUpdateRequest.UserID
        };
}
