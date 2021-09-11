using System;
using System.Collections.Generic;
using AutoMapper;
using Resources.Data;
using Resources.Dtos;
using Resources.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Resources.Controllers
{   
    //api/resources
    [Route("api/resources")]
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
        public async Task<IActionResult> GetAllResourcesAsync()
        {
            var resourceItems = await _repository.GetAllResourcesAsync();
            return Ok(_mapper.Map<IEnumerable<ResourceReadDto>>(resourceItems));
        }

        //Get api/resources/{id}        
        [HttpGet("{id}", Name="GetResourceById")]
        public async Task<IActionResult> GetResourceByIdAsync(int id)
        {
            var resourceItem = await _repository.GetResourceByIdAsync(id);
            if(resourceItem != null)
            {
                return Ok(_mapper.Map<ResourceReadDto>(resourceItem));
            }
            return NotFound();
        }

        //POST api/resources/{id} 
        [HttpPost]
        public async Task<IActionResult> CreateResourceAsync([FromBody] ResourceCreateDto resourceCreateDto) 
        {
            var resourceModel = _mapper.Map<Resource>(resourceCreateDto);
            await _repository.CreateResourceAsync(resourceModel);
            await _repository.SaveChangesAsync();

            var resourceReadDto = _mapper.Map<ResourceReadDto>(resourceModel);

            return CreatedAtRoute(nameof(GetResourceByIdAsync), new {Id = resourceReadDto.Id},resourceReadDto);
        }

        //PUT api/resources/{id} 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateresourceAsync(int id,[FromBody] ResourceUpdateDto resourceUpdateDto) 
        {
            var resourceModelFromRepo = await _repository.GetResourceByIdAsync(id);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(resourceUpdateDto, resourceModelFromRepo);

            _repository.UpdateResource(resourceModelFromRepo);
        
            await _repository.SaveChangesAsync();

            return NoContent();

        }
        //DELETE api/resources/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resourceModelFromRepo = await _repository.GetResourceByIdAsync(id);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteResource(resourceModelFromRepo);
            await _repository.SaveChangesAsync();
            return NoContent();

        }
    }
}