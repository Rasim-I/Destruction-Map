namespace Destruction_Map.Models.Middleware;

/*
public class TimerMiddleware
{
    private RequestDelegate next;
    private ITimeService timeService;

    public TimerMiddleware(RequestDelegate next, ITimeService timeService)
    {
        this.next = next;
        this.timeService = timeService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/time")
            await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        else
            await next.Invoke(context);
    }
    
}
*/