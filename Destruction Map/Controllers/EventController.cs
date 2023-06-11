using Destruction_Map.Areas.Identity.Data;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Destruction_Map.Controllers;

public class EventController : Controller
{
    private readonly ILogger<MapController> _logger;
    private IEventService _eventService;
    private UserManager<ApplicationUser> _userManager;

    public EventController(ILogger<MapController> logger, IEventService eventService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _eventService = eventService;
        _userManager = userManager;
    }

    [Authorize]
    public IActionResult AddEvent()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddEvent(DateTime eventDate, string location, string description, BuildingType buildingType, WeaponSystem weaponSystem, string sources)
    {
        string[] sourcesSplit = sources.Split("\n");
        
        Console.WriteLine(eventDate +" "+  location +" "+ description +" "+ buildingType +" "+ weaponSystem +" "+ sourcesSplit.Length);

        string userId = _userManager.GetUserId(User);
        _eventService.CreateEvent(userId, eventDate, location, description, buildingType, weaponSystem, sources);  //Id was "TestUser"
        
        return View();
    }
    
    
}