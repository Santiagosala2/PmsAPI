using System;
using System.Collections.Generic;
using AutoMapper;
using Resources.Repo;
using Resources.Dtos;
using Resources.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PmsAPI.Filters;

namespace Resources.Controllers
{   
    //api/resources
    [Route("api/resources")]
    [ApiController]
    [CustomTokenAuthFilter]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesRepo _repository;
        private readonly IMapper _mapper;

        public ResourcesController(IResourcesRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockresourceerRepo _repository = new MockresourceerRepo();
        //Get api/resources
        [HttpGet]
        public async Task<IActionResult> GetAllResourcesAsync([FromHeader] int userId )
        {
            var resourceItems = await _repository.GetAllResourcesAsync(userId);
            return Ok(_mapper.Map<IEnumerable<ResourceReadDto>>(resourceItems));
        }

        //Get api/resources/{id}        
        [HttpGet("{id}", Name= "GetResourceByIdAsync")]
        public async Task<IActionResult> GetResourceByIdAsync(int id , [FromHeader] int userId)
        {
            var resourceItem = await _repository.GetResourceByIdAsync(id , userId);
            if(resourceItem != null)
            {
                return Ok(_mapper.Map<ResourceReadDto>(resourceItem));
            }
            return NotFound();
        }

        //POST api/resources/
        [HttpPost]
        public async Task<IActionResult> CreateResourceAsync([FromHeader] int userId,[FromBody] ResourceCreateDto resourceCreateDto) 
        {
            var resourceModel = _mapper.Map<Resource>(resourceCreateDto);
            var success =  await _repository.CreateResourceAsync(userId , resourceModel);
            var resourceReadDto = _mapper.Map<ResourceReadDto>(resourceModel);
            if (success)  return CreatedAtRoute(nameof(GetResourceByIdAsync), new {Id = resourceReadDto.ResourceID },resourceReadDto);
            string errorMessage = "Something went wrong creating the resource";
            return BadRequest(new { errorMessage });
        }

        //PUT api/resources/{id} 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceAsync(int id,[FromBody] ResourceUpdateDto resourceUpdateDto, [FromHeader] int userId) 
        {
            var resourceModelFromRepo = await _repository.GetResourceByIdAsync(id , userId);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(resourceUpdateDto, resourceModelFromRepo);

            _repository.UpdateResource(resourceModelFromRepo);
        
            var saved = await _repository.SaveChangesAsync();

            return NoContent();

        }
        //DELETE api/resources/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id, [FromHeader] int userId)
        {
            var resourceModelFromRepo = await _repository.GetResourceByIdAsync(id , userId);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }
            var deleted = await _repository.DeleteResourceAsync(resourceModelFromRepo);
            if (deleted)
            {
                await _repository.SaveChangesAsync();
                return NoContent();

            }
            string errorMessage = "Something went wrong when deleting the resource";
            return BadRequest(new { errorMessage });

        }
    }
}