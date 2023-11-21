using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Abstract;

public interface IUserRules
{
    public void EmailMustBeUnique(string email);

    public void UserIsPresent(int id);
}
