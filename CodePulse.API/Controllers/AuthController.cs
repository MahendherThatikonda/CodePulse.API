using CodePulse.API.Models.DTO;
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

    public AuthController(UserManager<IdentityUser> userManager)
    {
      this.userManager = userManager;
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
