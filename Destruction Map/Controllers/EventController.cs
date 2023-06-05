using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Destruction_Map.Controllers;

public class EventController : Controller
{
    private readonly ILogger<MapController> _logger;
    private IEventService _eventService;
    
    public EventController(ILogger<MapController> logger, IEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
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
        
        _eventService.CreateEvent("TestUser", eventDate, location, description, buildingType, weaponSystem, sources);
        
        return View();
    }
    
    
}