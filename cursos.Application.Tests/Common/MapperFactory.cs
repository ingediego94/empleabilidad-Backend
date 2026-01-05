using AutoMapper;
using cursos.Application.DTOs;

public static class MapperFactory
{
    public static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapProfile>();
        });

        return config.CreateMapper();
    }
}