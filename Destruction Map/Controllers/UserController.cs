using Destruction_Map.Areas.Identity.Data;
using Destruction_Map.Models;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Destruction_Map.Controllers;


public class UserController : Controller
{
    
    private readonly ILogger<MapController> _logger;
    private IUserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private UserWebModelMapper _userWebModelMapper = new UserWebModelMapper();

    public UserController(ILogger<MapController> logger, IUserService userService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userService = userService;
        _userManager = userManager;
    }

    [HttpPost]
    public IActionResult UpdateUser(string userId, string name, string surname, string address)
    {
        _userService.UpdateUser(userId, name, surname, address);

        Console.WriteLine(userId + " --- " + name + " --- " + surname + " --- " + address);
        
        return RedirectToAction("UserPage");
    }

    public IActionResult UserPage()
    {
        string id = _userManager.GetUserId(User);
        string email = _userManager.GetUserName(User);
        //Console.WriteLine( "------------------" + id);
        User user =  _userService.GetUserById(id);
        UserWebModel userWebModel = _userWebModelMapper.ToWebModel(user, email);
        
        return View(userWebModel);
    }
    
}