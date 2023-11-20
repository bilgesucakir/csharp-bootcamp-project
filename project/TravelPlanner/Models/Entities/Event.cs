using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Event: Entity<Guid>
{
    //guid
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }

    //trip-event
    public Guid TripID { get; set; } //foreign key
    public Trip Trip { get; set; }

}
