using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record TripDetailDto
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get; set; }

    public string UserSurname { get; set; }
}