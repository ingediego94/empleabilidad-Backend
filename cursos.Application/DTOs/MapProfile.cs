using AutoMapper;
using cursos.Domain.Entities;

namespace cursos.Application.DTOs;

public class MapProfile : Profile
{
    public MapProfile()
    {
        
        // JWT
        CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
        
        CreateMap<User, UserRegisterResponseDto>();
        CreateMap<UserRegisterResponseDto, User>();
        
        CreateMap<UserAuthResponseDto, User>();
        CreateMap<User, UserAuthResponseDto>();
        
        
        // User:
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, ResponseUserDto>();
        
        // Course:
        CreateMap<CourseCreateDto, Course>();
        CreateMap<CourseUpdateDto, Course>();
        CreateMap<Course, ResponseCourseDto>();
        
        // Lesson:
        // CreateMap<UserCreateDto, Lesson>();
        // CreateMap<UserUpdateDto, Lesson>();
        // CreateMap<User, ResponseUserDto>();
    }
}