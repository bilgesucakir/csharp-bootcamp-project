using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Abstract;

public interface ITripRules
{
    public void StartDateMustBeSmallerThanEndDate(DateTime start, DateTime end);

    public void TripIsPresent(Guid id);
}
