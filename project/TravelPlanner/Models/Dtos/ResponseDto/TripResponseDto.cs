using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record TripResponseDto(
    Guid Id, string Title, DateTime StartDate, 
    DateTime EndDate, decimal Budget, int UserID)
{
    public static implicit operator TripResponseDto(Trip trip)
        {
        return new TripResponseDto(
           Id: trip.Id,
           Title: trip.Title,
           StartDate: trip.StartDate,
           EndDate: trip.EndDate,
           Budget: trip.Budget,
           UserID: trip.UserID
           );
        
    }
}
