using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jwt.Business.Abstract;
using Jwt.Business.Dtos.Person;
using System;
using System.Threading.Tasks;
using PBS.Business.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jwt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public readonly IPersonService _personservice;
        public readonly IAppUserService _appUserService;
        

        public PersonController(IPersonService personService, IAppUserService appUserService)
        {
            _personservice = personService;
            _appUserService = appUserService;
            
        }

       

 

        //[Authorize(Roles = "Admin")]
        [HttpPost("AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddPersonAsync(PersonAddDto personAddDto)
        {
            //var validator = new PersonAddValidator(_personservice);
            //var validatorResults = validator.Validate(personAddDto);

            //if (!validatorResults.IsValid)
            //{
            //    return BadRequest(validatorResults);
            //}

            try
            {

                await _personservice.AddPersonAsync(personAddDto);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

      
    }
}
