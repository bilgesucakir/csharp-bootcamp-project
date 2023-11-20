using Core.Persistence.EntityBaseModel;
using Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Excursion: Entity<Guid>
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
    public static implicit operator Excursion(ExcursionAddRequest excursionAddRequest)
        => new Excursion
        {
            Name = excursionAddRequest.Name,
            StartDate = excursionAddRequest.StartDate,
            EndDate = excursionAddRequest.EndDate,
            Location = excursionAddRequest.Location,
            Description = excursionAddRequest.Description,
            Cost = excursionAddRequest.Cost,
            TripID = excursionAddRequest.TripID
        };

    public static implicit operator Excursion(ExcursionUpdateRequest excursionUpdateRequest)
        => new Excursion
        {
            Id = excursionUpdateRequest.Id,
            Name = excursionUpdateRequest.Name,
            StartDate = excursionUpdateRequest.StartDate,
            EndDate = excursionUpdateRequest.EndDate,
            Location = excursionUpdateRequest.Location,
            Description = excursionUpdateRequest.Description,
            Cost = excursionUpdateRequest.Cost,
            TripID = excursionUpdateRequest.TripID
        };
}
