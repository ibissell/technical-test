using Bissell.Core.Models;
using Bissell.Services.DataTransferObjects;
using Bissell.Services.Interfaces;
using Bissell.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public IPersonService PersonService { get; set; }

        public PersonController(IPersonService personService)
        {
            PersonService = personService;
        }

        [HttpPost("_search")]
        [ProducesResponseType(typeof(IPagedList<PersonDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Get(PersonSearchParameters searchParameters)
        {
            try
            {
                IPagedList<PersonDto> personDtos = await PersonService.SearchAsync(searchParameters);

                return Ok(personDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Get(int personId)
        {
            try
            {
                PersonDto? personDto = await PersonService.GetAsync(personId);

                if (personDto != null)
                    return Ok(personDto);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(typeof(PersonDto), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> Create(PersonDto personDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personDto = await PersonService.CreateAsync(personDto);

                    return Ok(personDto);
                }
                else
                {
                    return BadRequest(personDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{personId}")]
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(typeof(PersonDto), 400)]
        [ProducesResponseType(typeof(PersonDto), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> Update(int personId, [FromBody] PersonDto personDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonDto? updatedPersonDto = await PersonService.UpdateAsync(personDto);

                    if (updatedPersonDto != null)
                        return Ok(updatedPersonDto);
                    else
                        return NotFound(personDto);
                }
                else
                {
                    return BadRequest(personDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{personId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Delete(int personId)
        {
            try
            {
                bool isDeleted = await PersonService.DeleteAsync(personId);

                if (isDeleted)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
