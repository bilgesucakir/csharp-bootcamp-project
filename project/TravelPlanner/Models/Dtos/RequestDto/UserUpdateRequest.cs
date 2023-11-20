using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.RequestDto
{
    public record UserUpdateRequest(int Id, string Name, string Surname, string Email)
    {
    }
}
