using Core.Persistence.EntityBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Trip: Entity<Guid>
{
    //guid
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Budget { get; set; }

    //user-trip
    public int UserID { get; set; } //foreign key
    public User User { get; set; }

    //trip-event
    public List<Activity> Activities { get; set;}


}
