using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cursos.Application.DTOs;
using cursos.Application.Interfaces;

namespace cursos.Api.Controllers;

[ApiController]
[Route("api/lessons")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _service;

    public LessonController(ILessonService service)
    {
        _service = service;
    }

    // ===============================
    // GET ALL
    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetAll()
    {
        var lessons = await _service.GetAllAsync();
        return Ok(lessons);
    }

    // ===============================
    // GET BY ID
    [HttpGet("{id}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var lesson = await _service.GetByIdAsync(id);
        if (lesson == null)
            return NotFound();

        return Ok(lesson);
    }

    // ===============================
    // CREATE
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] LessonCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    // ===============================
    // UPDATE
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] LessonUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // ===============================
    // DELETE (SOFT DELETE)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
