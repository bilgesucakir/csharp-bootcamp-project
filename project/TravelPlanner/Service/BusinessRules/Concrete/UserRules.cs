using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Service.Rules.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Rules.Concrete;

public class UserRules : IUserRules
{
    private readonly IUserRepository _userRepository;

    public UserRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void UserIsPresent(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            throw new BusinessException($"No user found with id: {id}");
        }
    }

    public void EmailMustBeUniqueForAdd(string email)
    {
        var user = _userRepository.GetByFilter(x => x.Email == email);
        if (user != null)
        {
            throw new BusinessException("Email must be unique");
        }
    }

    public void EmailMustBeUniqueForUpdate(string email, int id)
    {
        var user = _userRepository.GetByFilter(x => x.Email == email && x.Id != id);
        if (user != null)
        {
            throw new BusinessException("Email must be unique");
        }
    }
}
