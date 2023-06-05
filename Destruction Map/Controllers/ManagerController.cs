using Destruction_Map.Areas.Identity.Data;
using DestructionMapModel.Abstraction.IServices;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Destruction_Map.Controllers;

[Authorize(Policy = "ManagerOnly")]  //
public class ManagerController : Controller
{
    
    private readonly ILogger<MapController> _logger;
    private IEventService _eventService;
    private IUserService _userService;
    private IManagerService _managerService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ManagerController(ILogger<MapController> logger, IEventService eventService, 
        IUserService userService, IManagerService managerService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _eventService = eventService;
        _userService = userService;
        _managerService = managerService;
        _userManager = userManager;
    }

    //[Authorize]
    public IActionResult ApproveEvent()
    {
        string userId = _userManager.GetUserId(User);
        List<Event> eventsToApprove = _eventService.GetEventsToApprove(userId);

        return View(eventsToApprove);
    }

    [HttpPost]
    public IActionResult Approve(string eventId)
    {
        string userId =_userManager.GetUserId(User);

        //_managerService.CreateManager(userId);
        //_managerService.CreateManager(userId2);
        
        _managerService.VoteForEvent(eventId, userId, true);
        
        return RedirectToAction("ApproveEvent");
    }

    [HttpPost]
    public IActionResult Decline(string eventId)
    {

        string userId = _userManager.GetUserId(User);
        _managerService.VoteForEvent(eventId, userId, false);

        return RedirectToAction("ApproveEvent");
    }
    
}