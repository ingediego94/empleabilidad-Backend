using AutoMapper;
using cursos.Application.DTOs;
using cursos.Application.Services;
using cursos.Domain.Entities;
using cursos.Domain.Interfaces;
using Moq;
using Xunit;

public class LessonServiceTests
{
    private readonly Mock<IGeneralRepository<Lesson>> _repoMock;
    private readonly IMapper _mapper;
    private readonly LessonService _service;

    public LessonServiceTests()
    {
        _repoMock = new Mock<IGeneralRepository<Lesson>>();
        _mapper = MapperFactory.Create();
        _service = new LessonService(_repoMock.Object, _mapper);
    }

    [Fact]
    public async Task CreateLesson_ShouldSucceed()
    {
        var dto = new LessonCreateDto
        {
            CourseId = 1,
            Title = "Intro",
            Order = 1
        };

        var result = await _service.CreateAsync(dto);

        Assert.Equal("Intro", result.Title);
    }

    [Fact]
    public async Task UpdateLesson_WhenDeleted_ShouldFail()
    {
        var lesson = new Lesson { Id = 1, IsDeleted = true };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(lesson);

        var result = await _service.UpdateAsync(1, new LessonUpdateDto());

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteLesson_ShouldReturnFalse_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Lesson?)null);

        var result = await _service.DeleteAsync(1);

        Assert.False(result);
    }
    
    [Fact]
    public async Task CreateLesson_WithDuplicateOrder_ShouldFail()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Lesson>
        {
            new Lesson { CourseId = 1, Order = 1 }
        });

        var dto = new LessonCreateDto { CourseId = 1, Order = 1 };

        await Assert.ThrowsAsync<Exception>(() => _service.CreateAsync(dto));
    }

}