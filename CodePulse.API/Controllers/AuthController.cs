using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenRepository tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
      this.userManager = userManager;
      this.tokenRepository = tokenRepository;
    }

    //post:apibaseURl/api/auth/login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
      //Check Email
      var identityUser =await userManager.FindByEmailAsync(request.Email);
      if (identityUser is not null) {
        //Check Password
        var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser,request.Password);

        if (checkPasswordResult)
        {
          var roles = await userManager.GetRolesAsync(identityUser);

          //Create Token and Response
          var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

          var response = new LoginResponseDto()
          {
            Email = request.Email,
            Roles = roles.ToList(),
 //           Token = jwtToken
          };
          //create a token
          return Ok(response);
        }
      }
      ModelState.AddModelError("", "Email or Password Incorrect");

      return ValidationProblem(ModelState);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
      //Create Identity user Object

      var user = new IdentityUser
      {
        UserName = request.Email?.Trim(),
        Email = request.Email?.Trim()
      };

      var identityresult = await userManager.CreateAsync(user, request.Password);
      if (identityresult.Succeeded)
      {
        //Add role to the user
        await userManager.AddToRoleAsync(user, "Reader");

        if (identityresult.Succeeded)
        {
          return Ok();
        }
        else
        {
          if (identityresult.Errors.Any())
          {
            foreach (var error in identityresult.Errors)
            {
              ModelState.AddModelError("", error.Description);
            }
          }
        }
      }
      else
      {
        if (identityresult.Errors.Any())
        {
          foreach (var error in identityresult.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
        }
      }

      return ValidationProblem(ModelState);
    }
  }
}
