using Lavenue.AuthenticationService.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lavenue.AuthenticationService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IAccountService _accountService;

    public UserController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _accountService.GetAllUsers();
        return Json(result);
    }

}