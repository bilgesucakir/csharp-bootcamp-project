using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record UserResponseDto(int Id, string Name, string Surname, string Email)
{
    public static implicit operator UserResponseDto(User user)
    {
        return new UserResponseDto(
            Id: user.Id,
            Name: user.Name,
            Surname: user.Surname,
            Email: user.Email
        );
    }
}