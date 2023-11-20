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

}
