using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecondTask.Models;
using SecondTask.Services;

namespace SecondTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JwtController : ControllerBase
{
    private ILogger<JwtController> _logger;
    private IJwtService _jwtService;
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public JwtController(
    ILogger<JwtController> logger,
    IJwtService jwtService,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(User user)
    {
        var userw = new IdentityUser
        {
            UserName = user.Username,
            Email = user.Email,
            PasswordHash = user.Password
        };

        var result = await _userManager.FindByNameAsync(user.Username);

        if (!(result is null))
            return BadRequest("This user exist");
            
        var cretedUser = await _userManager.CreateAsync(userw);
        if (!cretedUser.Succeeded)
        {
            _logger.LogInformation($"Not succeed {cretedUser}");
            return BadRequest($"This user not created {user.Username}");
        }

        List<string> statusCodes = new List<string>();

        statusCodes.Add(StatusCode(200).ToString()!);

        return Ok(statusCodes);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUser user1)
    {
        if (user1 is null)
            return BadRequest();

        var existUser = await _userManager.FindByNameAsync(user1.Username);
        if (existUser is null)
            return NotFound();


        var identityUser = new IdentityUser(user1.Username);


        var signedUser =  await _signInManager.CheckPasswordSignInAsync(identityUser,user1.Password,false);
        
        if(signedUser is null)
        return BadRequest("username or password false");
        var existuser = await _userManager.FindByNameAsync(user1.Username);

        var user = new User
        {
            Username = existUser.UserName,
            Password = existUser.PasswordHash,
        };

        var token = _jwtService.GenerateJwt(user);

        if (token is null)
            return BadRequest("Token is not generated");

        List<string> list = new List<string>();
        list.Add(token);

        return Ok(list);
    }


}