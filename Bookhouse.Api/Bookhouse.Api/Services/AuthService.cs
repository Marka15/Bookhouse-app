using Bookhouse.Api.Data;
using Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
using Bookhouse.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookhouse.Api.Services;
public class AuthService : IAuthService
    {
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null) return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!isValid) return null;

        var token = GenerateToken(user);

        return new AuthResponseDto
        {
            Email = user.Email,
            Token = token
        };
    }

    private string GenerateToken(User user) {
        var claims = new[]
        {
         new Claim(ClaimTypes.Email, user.Email),
         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
         );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
           issuer: _configuration["Jwt:Issuer"],
           audience: _configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddHours(24),
           signingCredentials: credentials
       );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

