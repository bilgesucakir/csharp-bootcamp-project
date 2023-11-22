using Core.Shared;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract;

public interface IUserService
{
    Response<UserResponseDto> Add(UserAddRequest userAddRequest);
    Response<UserResponseDto> Update(UserUpdateRequest userUpdateRequest);
    Response<UserResponseDto> Delete(int id);
    Response<UserResponseDto> GetById(int id);
    Response<List<UserResponseDto>> GetAll();

    Response<List<UserResponseDto>> GetAllFiltered(string? name, string? surname);

}
