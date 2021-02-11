using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOS;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Person> _personService;

        public PersonsController(IService<Person> personService, IMapper mapper)
        {
            _mapper = mapper;
            _personService = personService;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));
        }
        
        /// <summary>
        /// Save a person
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(PersonDto personDto)
        {
            return Created( string.Empty,_mapper.Map<PersonDto>(await _personService.AddAsync(_mapper.Map<Person>(personDto))));
        }



    }
}
