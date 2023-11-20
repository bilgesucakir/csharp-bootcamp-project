using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.RequestDto;

public record ActivityUpdateRequest(Guid Id, string Name, DateTime StartDate, DateTime EndDate,
    string Location, string Description, decimal Cost, Guid TripID)
{

    public static Activity ConvertToEntity(ActivityUpdateRequest activityUpdateRequest)
    {
        return new Activity
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
}