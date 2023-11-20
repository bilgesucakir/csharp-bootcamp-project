using Core.Persistence.EntityBaseModel;
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

}
