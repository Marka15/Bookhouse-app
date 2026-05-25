namespace Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
 public interface IAuthService
 {
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);

 }

