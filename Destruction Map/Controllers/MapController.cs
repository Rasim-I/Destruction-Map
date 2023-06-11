using System.Diagnostics;
using Destruction_Map.Areas.Identity.Data;
using Destruction_Map.Data;
using Microsoft.AspNetCore.Mvc;
using Destruction_Map.Models;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Destruction_Map.Controllers;

public class MapController : Controller
{
    private readonly ILogger<MapController> _logger;
    private IEventService _eventService;
    private EventWebModelMapper _eventWebModelMapper = new EventWebModelMapper();
    private readonly UserManager<ApplicationUser> _userManager;

    public MapController(ILogger<MapController> logger, IEventService eventService, 
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _eventService = eventService;
        _userManager = userManager;
    }
    
    
    public IActionResult Index()
    {
        //ViewBag.UserId = _userManager.GetUserId(User); //Only if logged
        
        
        ColorMap();
        return View();
    }

    [NonAction]
    public void ColorMap()
    {
        Dictionary<string, int> eventsIntensity = _eventService.GetEventsIntensity();

        foreach (var location in eventsIntensity)
        {
            ViewData[location.Key] = location.Value;
            
            //Console.WriteLine(location.Key + " ---- " + location.Value);
        }
    }
    
    public IActionResult Privacy()
    {
        //_eventService.CreateEvent();
        return View();
    }

    public IActionResult InfoBox()
    {
        return PartialView();
    }
    
    [HttpGet]
    public IActionResult SearchByKeyWords(string keyWords)
    {
        List<Event> filteredList = _eventService.FindByKeyWords(keyWords);
        ViewBag.Events = filteredList;
        
        ColorMap();
        return View("Index", filteredList);
    }

    /*
    public IActionResult DisplayData()
    {
        ViewBag.Events = new List<Event>
        {
            new Event("useruser123", DateTime.Today, "Chernihiv", "something happened", BuildingType.Commercial,
                WeaponSystem.Ballistic_missile)
        };
        return RedirectToAction("Index");
    }
*/
    
    public IActionResult GetEvents(string location, int page = 1)
    {
        
        List<EventWebModel> list = _eventService.GetByLocation(location).ConvertAll(e=> _eventWebModelMapper.ToEventWebModel(e));
        
        ColorMap();

        return View("Index", list);
        //return RedirectToAction("Index", list);
    }

    [HttpGet]
    public IActionResult SearchByParams(List<BuildingType> selectedBuildings, List<WeaponSystem> selectedWeaponSystems, string location, DateTime date)
    {
        EventParameters eventParameters = new EventParameters()
        {
            Location = location, EventDate = date, BuildingType = selectedBuildings,
            WeaponSystem = selectedWeaponSystems
        };

        List<EventWebModel> list = _eventService.FindByParameters(eventParameters).ConvertAll(e => _eventWebModelMapper.ToEventWebModel(e));
        
        foreach (var building in selectedBuildings)
        {
            //Console.WriteLine(building.ToString());

        }

        ColorMap();
        return View("Index", list);
    }

    public IActionResult ShowParams()
    {
        //ViewBag.ShowParams = true;
        EventWebModel eventModel = new EventWebModel() { Id = "3rf234f34" };
        return PartialView(eventModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}