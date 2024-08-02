using AIM.Data;
using AIM.Dtos;

namespace AIM.Interface;

public interface IAuthService
{
    Task<AuthServiceResponseDto> SeedRolesAsync();
    Task<RegisterAuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
    Task<MakeAdminOwnerResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
    Task<MakeAdminOwnerResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);
    Task<RefreshAuthResponseDto> RefreshTokenAsync(TokenRefreshDto tokenRefreshDto);
}