namespace Destruction_Map.Models.Middleware;

public class RoutingMiddleware
{
    public RoutingMiddleware(RequestDelegate _)
    {
        
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;
        if (path == "/index")
            await context.Response.WriteAsync("Home page");
        else if (path == "/about")
            await context.Response.WriteAsync("About page");
        else
            context.Response.StatusCode = 404;

    }
    
    
}