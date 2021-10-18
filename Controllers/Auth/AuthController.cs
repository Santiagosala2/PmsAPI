using Auth;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Dtos;
using Users.Models;
using Tokens.Dtos;

namespace Auth.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;
        private readonly IMapper _mapper;

        public AuthController(ICustomUserManager customUserManager,
            ICustomTokenManager customTokenManager, IMapper mapper)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserReadDto user)
        {
            var userModel = _mapper.Map<User>(user);
            userModel.SaltedHashedPassword = user.Password;
            var (authResult, token) = await _customUserManager.AuthenticateAsync(userModel.Email, userModel.SaltedHashedPassword);
            string errorMessage = "User could not be authenticated";
            if (authResult)
            {
               return Ok(new { token });
            }           
            return BadRequest(new { errorMessage }); 
        }

        [HttpPost("verifytoken")]
        public async Task<IActionResult> VerifyAsync([FromBody] TokenReadDto token)
        {
            var result = await _customTokenManager.VerifyTokenAsync(token.TokenString,null);
            return Ok(new { result });

        }

        [HttpPost("getinfo")]
        public async Task<IActionResult> GetUserInfoByTokenAsync([FromBody] TokenReadDto token)
        {
            var result = await _customTokenManager.GetUserInfoByTokenAsync(token.TokenString);
           
            if ( result is not null)
            {
              var userModel = _mapper.Map<UserInfoDto>(result);
              return Ok(new { userModel });

            }
            var errorMessage = "User not found or token has already expired";
            return NotFound(new { errorMessage });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto user)
        {
            string errorMessage = "";
            var userModel = _mapper.Map<User>(user);
            userModel.SaltedHashedPassword = user.Password;
            

            if (userModel is not null)
            {
                bool result = false;
                string token = "";

                if (userModel.Username is not null && userModel.SaltedHashedPassword is not null && userModel.Email is not null)
                {
                    (result , token ) = await _customUserManager.CreateUserAsync(userModel);
                }

                if (result)
                {
                    return Ok(new { token });
                }

                if (userModel.Username is null)
                {
                    errorMessage = "Username is null";

                }
                else if (userModel.SaltedHashedPassword is null)
                {
                    errorMessage = "Password is null";

                }
                else if (userModel.Email is null)
                {
                    errorMessage = "Email is null";
                }
                else
                {
                    errorMessage = "User already exists";
                }

            }

            return BadRequest(new { errorMessage });

        }
    }
}
