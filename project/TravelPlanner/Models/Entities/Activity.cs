using Core.Persistence.EntityBaseModel;
using Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Activity: Entity<Guid>
{
    //guid
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }

    //trip-event
    public Guid TripID { get; set; } //foreign key
    public Trip Trip { get; set; }

    //operators
    public static implicit operator Activity(ActivityAddRequest activityAddRequest)
        => new Activity
        {
            Name = activityAddRequest.Name,
            StartDate = activityAddRequest.StartDate,
            EndDate = activityAddRequest.EndDate,
            Location = activityAddRequest.Location,
            Description = activityAddRequest.Description,
            Cost = activityAddRequest.Cost,
            TripID = activityAddRequest.TripID
        };

    public static implicit operator Activity(ActivityUpdateRequest activityUpdateRequest)
        => new Activity
        {
            Id = activityUpdateRequest.Id,
            Name = activityUpdateRequest.Name,
            StartDate = activityUpdateRequest.StartDate,
            EndDate = activityUpdateRequest.EndDate,
            Location = activityUpdateRequest.Location,
            Description = activityUpdateRequest.Description,
            Cost = activityUpdateRequest.Cost,
            TripID = activityUpdateRequest.TripID
        };
}
