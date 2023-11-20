using Core.Persistence.EntityBaseModel;
using Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class User: Entity<int>
{
    //int
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    //user-trip
    public List<Trip> Trips { get; set; }

    //operators
    public static implicit operator User(UserAddRequest userAddRequest)
        => new User
        {
            Name = userAddRequest.Name,
            Surname = userAddRequest.Surname,
            Email = userAddRequest.Email
        };

    public static implicit operator User(UserUpdateRequest userUpdateRequest)
        => new User
        {
            Id = userUpdateRequest.Id,
            Name = userUpdateRequest.Name,
            Surname = userUpdateRequest.Surname,
            Email = userUpdateRequest.Email
        };
}
