using Bissell.Core.Models;
using Bissell.Services.DataTransferObjects;
using Bissell.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        public IBugService BugService { get; set; }

        public BugController(IBugService bugService)
        {
            BugService = bugService;
        }

        [HttpPost("_search")]
        [ProducesResponseType(typeof(IPagedList<BugDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Get(BugSearchParameters searchParameters)
        {
            try
            {
                IPagedList<BugDto> bugDtos = await BugService.SearchAsync(searchParameters);

                return Ok(bugDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{bugId}")]
        [ProducesResponseType(typeof(BugDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Get(int bugId)
        {
            try
            {
                BugDto? bugDto = await BugService.GetAsync(bugId);

                if (bugDto != null)
                    return Ok(bugDto);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BugDto), 200)]
        [ProducesResponseType(typeof(BugDto), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> Create(BugDto bugDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bugDto = await BugService.CreateAsync(bugDto);

                    return Ok(bugDto);
                }
                else
                {
                    return BadRequest(bugDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{bugId}")]
        [ProducesResponseType(typeof(BugDto), 200)]
        [ProducesResponseType(typeof(BugDto), 400)]
        [ProducesResponseType(typeof(BugDto), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [Consumes("application/json", "multipart/form-data")]
        public async Task<IActionResult> Update(int bugId, [FromBody] BugDto bugDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BugDto? updatedBugDto = await BugService.UpdateAsync(bugDto);

                    if (updatedBugDto != null)
                        return Ok(updatedBugDto);
                    else
                        return NotFound(bugDto);
                }
                else
                {
                    return BadRequest(bugDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{bugId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Delete(int bugId)
        {
            try
            {
                bool isDeleted = await BugService.DeleteAsync(bugId);

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

        [HttpPost("{bugId}/update/{status}")]
        [ProducesResponseType(typeof(BugDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Update(int bugId, [FromQuery] BugStatus status)
        {
            try
            {
                BugDto? updatedBugDto = await BugService.UpdateAsync(bugId, status);

                if (updatedBugDto != null)
                    return Ok(updatedBugDto);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{bugId}/assign/{personid}")]
        [ProducesResponseType(typeof(BugDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> Assign(int bugId, [FromQuery] int personId)
        {
            try
            {
                BugDto? updatedBugDto = await BugService.AssignAsync(bugId, personId);

                if (updatedBugDto != null)
                    return Ok(updatedBugDto);
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
