using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record ExcursionDetailDto
{
    public Guid Id {get; init;}
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }

    public string TripTitle { get; set; }
    public DateTime TripStartDate { get; set;}
    public DateTime TripEndDate { get; set; }
};
