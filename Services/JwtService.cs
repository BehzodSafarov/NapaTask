using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SecondTask.Models;

namespace SecondTask.Services;

public class JwtService : IJwtService
{
    private IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string? GenerateJwt(User user)
    {
        var claims = new List<Claim>
        {
          new Claim("UserName", user.Username!),
          new Claim("Password", user.Password!)
        };

        var claim = new Claim(ClaimTypes.Role, "Admin");
        claims.Add(claim);
        var claim1 = new Claim(ClaimTypes.Role, "User");
        claims.Add(claim1);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
         var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateJwt(string token)
    {
        var jwtSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

         var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(_configuration["JWT:Key"],
                  new TokenValidationParameters
                {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateIssuerSigningKey = true,
                 ValidateLifetime = true,
                 ValidIssuer = _configuration["JWT:Issuer"],
                 ValidAudience = _configuration["JWT:Audience"],
                 IssuerSigningKey = jwtSecret
                },
                out SecurityToken validateToken);
            }
            catch (System.Exception)
            {
                
                return false;
            }
        return true;
    }
}