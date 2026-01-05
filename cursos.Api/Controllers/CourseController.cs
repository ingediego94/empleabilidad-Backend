using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cursos.Application.DTOs;
using cursos.Application.Interfaces;
using cursos.Domain.Enum;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }


    // CREATE COURSE
    // POST /api/courses
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }


    // UPDATE COURSE
    // PUT /api/courses/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return Ok(result);
    }


    // PUBLISH COURSE
    // PATCH /api/courses/{id}/publish
    [HttpPatch("{id}/publish")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Publish(int id)
    {
        await _service.PublishAsync(id);
        return Ok(new { message = "Course published successfully" });
    }


    // UNPUBLISH COURSE
    // PATCH /api/courses/{id}/unpublish
    [HttpPatch("{id}/unpublish")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Unpublish(int id)
    {
        await _service.UnpublishAsync(id);
        return Ok(new { message = "Course unpublished successfully" });
    }
    
    
    // SUMMARY
    // GET /api/courses/{id}/summary
    [HttpGet("{id}/summary")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> Summary(int id)
    {
        var result = await _service.GetSummaryAsync(id);
        return Ok(result);
    }


    // SEARCH
    // GET /api/courses/search
    [HttpGet("search")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> Search(
        [FromQuery] string? q,
        [FromQuery] Status? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.SearchAsync(q, status, page, pageSize);
        return Ok(result);
    }
    
    
    

    // DELETE COURSE (SOFT DELETE)
    // DELETE /api/courses/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return BadRequest("Course could not be deleted");

        return NoContent(); 
    }

}
