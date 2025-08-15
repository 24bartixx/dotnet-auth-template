using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApiTemplate.Dtos;
using AuthApiTemplate.Dtos.Auth;
using AuthApiTemplate.Models;

namespace AuthApiTemplate.Mappers
{
    public static class AuthMappers
    {
        public static UserDto ToUserDtoFromRegister(this AppUser appUser)
        {
            return new UserDto
            {
                Email = appUser.Email ?? "",
                UserName = appUser.UserName ?? ""
            };
        }
    }
}