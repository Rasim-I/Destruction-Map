using Destruction_Map.Areas.Identity.Data;
using DestructionMapModel.Abstraction.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Destruction_Map.Controllers;


public class UserController : Controller
{
    
    private readonly ILogger<MapController> _logger;
    private IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(ILogger<MapController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
        
    }

    [HttpPost]
    public IActionResult UpdateUser(string userId, string name, string surname, string address, int age)
    {
        _userService.UpdateUser(userId, name, surname, age, address);

        return RedirectToAction("");
    }

    public IActionResult UserPage()
    {
        string id = _userManager.GetUserId(User);
        _userService.GetUserById(id);
        return View();
    }
    
}