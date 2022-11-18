using Microsoft.AspNetCore.Identity;
using SecondTask.Models;

namespace SecondTask.Services;

public interface IJwtService 
{
    public string? GenerateJwt(User user);
    public bool ValidateJwt(string token);
}