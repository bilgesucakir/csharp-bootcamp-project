using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Service.Abstract;
using Service.Rules.Abstract;
using Service.Rules.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRules _userRules;

    public UserService(IUserRepository userRepository, IUserRules userRules)
    {
        _userRepository = userRepository;
        _userRules = userRules;
    }

    public Response<UserResponseDto> Add(UserAddRequest userAddRequest)
    {
        try
        {
            User user = userAddRequest;

            _userRules.EmailMustBeUnique(user.Email);

            _userRepository.Add(user);

            UserResponseDto userResponse = user;

            return new Response<UserResponseDto>
            {
                Data = userResponse,
                Message = "User added",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<UserResponseDto> Delete(int id)
    {
        try
        {
            _userRules.UserIsPresent(id);

            var user = _userRepository.GetById(id);

            _userRepository.Delete(user);

            UserResponseDto userResponse = user;

            return new Response<UserResponseDto>
            {
                Data = userResponse,
                Message = "User deleted",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDto>
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<UserResponseDto>> GetAll()
    {
        var users = _userRepository.GetAll();
        var responses = users.Select(x => (UserResponseDto)x).ToList();

        return new Response<List<UserResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<List<UserResponseDto>> GetAllFiltered(string? name, string? surname)
    {
        var filteredUsers = _userRepository.GetAll();

        //applying filters based on the provided values
        if (!name.IsNullOrEmpty())
        {
            filteredUsers = filteredUsers.Where(x => x.Name == name).ToList();
        }

        if (!surname.IsNullOrEmpty())
        {
            filteredUsers = filteredUsers.Where(x => x.Surname == surname).ToList();
        }

        var responses = filteredUsers.Select(x => (UserResponseDto)x).ToList();

        return new Response<List<UserResponseDto>>()
        {
            Data = responses,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<UserResponseDto> GetById(int id)
    {
        try
        {
            _userRules.UserIsPresent(id);

            var user = _userRepository.GetById(id);

            UserResponseDto userResponse = user;

            return new Response<UserResponseDto>()
            {
                Data = userResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<UserResponseDto> Update(UserUpdateRequest userUpdateRequest)
    {
        try
        {
            User user = userUpdateRequest;

            _userRules.EmailMustBeUnique(user.Email);
            _userRepository.Update(user);

            UserResponseDto userResponse = user;

            return new Response<UserResponseDto>()
            {
                Data = userResponse,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<UserResponseDto>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
