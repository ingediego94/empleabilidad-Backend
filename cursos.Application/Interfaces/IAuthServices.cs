using cursos.Application.DTOs;

namespace cursos.Application.Interfaces;

public interface IAuthServices
{
    Task<UserAuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<UserRegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<UserAuthResponseDto> RefreshAsync(RefreshDto refreshDto);
    Task<bool> RevokeAsync(RevokeTokenDto revokeTokenDto);
}