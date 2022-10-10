using Lavenue.AuthenticationService.Model;
using Lavenue.AuthenticationService.Model.Dto;
using Lavenue.AuthenticationService.Service.Interfaces;
using Lavenue.Services.Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lavenue.AuthenticationService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Route("signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp()
    {
        var user = new UserDto
        {
            Email = "zgasmi.ca1754745t@yopmail.com",
            Name = "Zakaria GasmiTt",
            Password = "P@ssword1234DDD",
            FamilyName = "GasmiTt",
            GivenName = "ZakTt",
            UserName = "zgasmi12351t"
        };
        var created = await _accountService.SignUp(user);
        return created ? Ok() : BadRequest();
    }

    [Route("SignIn")]
    [HttpPost]
    public ActionResult<JwToken?> SignIn(User user)
    {
        var response = _accountService.SignIn(user);
        return response != null ? Json(response) : BadRequest();
    }
}