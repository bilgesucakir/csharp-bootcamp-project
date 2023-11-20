using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record ExcursionResponseDto(
    Guid Id, string Name, DateTime StartDate, DateTime EndDate,
    string Location, string Description, decimal Cost, Guid TripID)
{
    public static implicit operator ExcursionResponseDto(Excursion excursion)
    {
        return new ExcursionResponseDto(
            Id: excursion.Id,
            Name: excursion.Name,
            StartDate: excursion.StartDate,
            EndDate: excursion.EndDate,
            Location: excursion.Location,
            Description: excursion.Description,
            Cost: excursion.Cost,
            TripID: excursion.TripID
            );
    }
}
