using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.RequestDto;

public record TripUpdateRequest(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate,
    decimal Budget, int UserID)
{
    //User List<Activity>?
}
