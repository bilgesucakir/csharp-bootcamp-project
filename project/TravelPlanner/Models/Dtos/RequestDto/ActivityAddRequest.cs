using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.RequestDto;

public record ActivityAddRequest(string Name, DateTime StartDate, DateTime EndDate,
    string Location, string Description, decimal Cost, Guid TripID)
{

    public static Activity ConvertToEntity(ActivityAddRequest activityAddRequest)
    {
        return new Activity
        {
            Name = activityAddRequest.Name,
            StartDate = activityAddRequest.StartDate,
            EndDate = activityAddRequest.EndDate,
            Location = activityAddRequest.Location,
            Description = activityAddRequest.Description,
            Cost = activityAddRequest.Cost,
            TripID = activityAddRequest.TripID
        };
    }
}
