using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;
using Users.Repo;

namespace PmsAPI.Controllers
{
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepo repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
   
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            var result = await _repository.CreateUserAsync(user);

            if (result)
            {
                return Ok();
            }

            return BadRequest("User already exists perhaps");

        }
    }
}
