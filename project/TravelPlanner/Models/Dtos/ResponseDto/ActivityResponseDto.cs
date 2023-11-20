using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record ActivityResponseDto(
    Guid Id, string Name, DateTime StartDate, DateTime EndDate,
    string Location, string Description, decimal Cost, Guid TripID)
{
    public static implicit operator ActivityResponseDto(Activity activity)
    {
        return new ActivityResponseDto(
            Id: activity.Id,
            Name: activity.Name,
            StartDate: activity.StartDate,
            EndDate: activity.EndDate,
            Location: activity.Location,
            Description: activity.Description,
            Cost: activity.Cost,
            TripID: activity.TripID
            );
    }
}
