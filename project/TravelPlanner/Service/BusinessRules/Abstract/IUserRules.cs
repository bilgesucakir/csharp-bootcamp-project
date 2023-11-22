using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Abstract;

public interface IUserRules
{
    public void EmailMustBeUniqueForAdd(string email);

    public void EmailMustBeUniqueForUpdate(string email, int id);

    public void UserIsPresent(int id);
}
