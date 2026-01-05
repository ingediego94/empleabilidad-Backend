using AutoMapper;
using cursos.Application.Services;
using cursos.Domain.Entities;
using cursos.Domain.Enum;
using cursos.Domain.Interfaces;
using Moq;
using Xunit;

public class CourseServiceTests
{
    private readonly Mock<IGeneralRepository<Course>> _repoMock;
    private readonly IMapper _mapper;
    private readonly CourseService _service;

    public CourseServiceTests()
    {
        _repoMock = new Mock<IGeneralRepository<Course>>();
        _mapper = MapperFactory.Create();
        _service = new CourseService(_repoMock.Object, _mapper);
    }

    [Fact]
    public async Task PublishCourse_WithLessons_ShouldSucceed()
    {
        var course = new Course
        {
            Id = 1,
            Status = Status.Draft,
            Lessons = new List<Lesson>
            {
                new Lesson { IsDeleted = false }
            }
        };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(course);

        var result = await _service.PublishAsync(1);

        Assert.True(result);
        Assert.Equal(Status.Published, course.Status);
    }

    [Fact]
    public async Task PublishCourse_WithoutLessons_ShouldFail()
    {
        var course = new Course
        {
            Id = 1,
            Lessons = new List<Lesson>()
        };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(course);

        await Assert.ThrowsAsync<Exception>(() => _service.PublishAsync(1));
    }

    [Fact]
    public async Task DeleteCourse_ShouldBeSoftDelete()
    {
        var course = new Course { Id = 1 };

        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(course);
        _repoMock.Setup(r => r.DeleteAsync(course)).ReturnsAsync(true);

        var result = await _service.DeleteAsync(1);

        Assert.True(result);
        _repoMock.Verify(r => r.DeleteAsync(course), Times.Once);
    }
}