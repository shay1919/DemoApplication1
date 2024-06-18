using DemoApplication1.Models;
using DemoApplication1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        private readonly PersonService _personService;

        public static List<Person> personList = new List<Person>();

        public PersonController(PersonService personService, PersonRepository personRepository)
        {
            _personService = personService;
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var persons = await _personRepository.GetPersonsAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                Person person = personList.Where(person => person.Id == id).First();
                return Ok(person);
            }
            catch
            {
                return BadRequest($"Person with ID: {id} doesn't exist.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Person person)
        {
            try
            {
                _personRepository.AddPersonAsync(person);
                _personRepository.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Failed to create person.");
            }
            return Created(Request.Path.Value, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Person person, int id)
        {
            Person storedPerson = null;
            try
            {
                storedPerson = personList.Where(thisPerson => thisPerson.Id == id).First();
                storedPerson.FirstName = person.FirstName;
                storedPerson.LastName = person.LastName;
                storedPerson.DateOfBirth = person.DateOfBirth;
                storedPerson.Height = person.Height;
                storedPerson.Weight = person.Weight;
            }
            catch
            {
                storedPerson = person;
                personList.Add(storedPerson);
                return Created(Request.Path.Value, person);
            }
            return Ok(storedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            personList.RemoveAll(person => person.Id == id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult JsonPatchWithModelState([FromBody] JsonPatchDocument<Person> patchDoc, int id)
        {
            Person storedPerson = null;
            try
            {
                storedPerson = personList.Where(person => person.Id == id).First();
            }
            catch
            {
                return BadRequest(ModelState);
            }
            patchDoc.ApplyTo(storedPerson, ModelState);
            return Ok(storedPerson);
        }
    }
}
