using System;
using System.Collections.Generic;
using AutoMapper;
using Resources.Data;
using Resources.Dtos;
using Resources.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult <IEnumerable<ResourceReadDto>> GetAllResources()
        {
            var resourceItems = _repository.GetAllResources();
            return Ok(_mapper.Map<IEnumerable<ResourceReadDto>>(resourceItems));
        }

        //Get api/resources/{id}        
        [HttpGet("{id}", Name="GetResourceById")]
        public ActionResult <ResourceReadDto> GetResourceById(int id)
        {
            var resourceItem = _repository.GetResourceById(id);
            if(resourceItem != null)
            {
                return Ok(_mapper.Map<ResourceReadDto>(resourceItem));
            }
            return NotFound();
        }

        //POST api/resources/{id} 
        [HttpPost]
        public ActionResult <ResourceReadDto> CreateResource([FromBody] ResourceCreateDto resourceCreateDto) 
        {
            var resourceModel = _mapper.Map<Resource>(resourceCreateDto);
            _repository.CreateResource(resourceModel);
            _repository.SaveChanges();

            var resourceReadDto = _mapper.Map<ResourceReadDto>(resourceModel);

            return CreatedAtRoute(nameof(GetResourceById), new {Id = resourceReadDto.Id},resourceReadDto);
        }

        //PUT api/resources/{id} 
        [HttpPut("{id}")]
        public ActionResult <ResourceUpdateDto> Updateresource(int id,[FromBody] ResourceUpdateDto resourceUpdateDto) 
        {
            var resourceModelFromRepo = _repository.GetResourceById(id);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(resourceUpdateDto, resourceModelFromRepo);

            _repository.UpdateResource(resourceModelFromRepo);
        
            _repository.SaveChanges();

            return NoContent();

        }
        //DELETE api/resources/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteResource(int id)
        {
            var resourceModelFromRepo = _repository.GetResourceById(id);
            if(resourceModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteResource(resourceModelFromRepo);
            _repository.SaveChanges();
            return NoContent();

        }
    }
}