using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Abstract;

public interface IExcursionRules
{

    public void StartDateMustBeSmallerThanEndDate(DateTime start, DateTime end);

    public void ExcursionIsPresent(Guid id);
}
