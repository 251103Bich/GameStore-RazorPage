using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Middleware
{
    public class CookieCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var url = context.Request.Path.ToString().ToUpper();
            var user = context.Request.Cookies["Username"];
            var profileService = context.RequestServices.GetService<IProfileService>();

            if ((user == null && url.Contains("/HOME/")) || (url.Contains("/HOME/LOGOUT") || (url.Contains("/HOME/DISABLE"))) || (url.Contains("/GAME/") && user == null) || (user == null && url.Contains("/GAME"))) { 
                await _next(context);
                return;
            }
            if (user == null && !url.Contains("GOOGLE.COM"))
            {
                await _next(context);
                return;
            }
            if (user == null) {
                context.Response.Redirect("/Home/Index");
                return;
            }
            var profile = await profileService.GetProfileByUsername(user);
            if(profile.Status == "Disable")
            {
                context.Response.Redirect("/Home/Disable");
                return;
            }
            if (profile.Type == "Seller")
            {
                if (url.Contains("/MANAGE/"))
                {
                    await _next(context);
                    return;
                }
                context.Response.Redirect("/MANAGE/GAME");
                return;
            }
            if (profile.Type == "Admin")
            {
                if (url.Contains("/ADMINISTRATOR/"))
                {
                    await _next(context);
                    return;
                }
                context.Response.Redirect("/ADMINISTRATOR/REVENUE");
                return;
            }
            if(profile.Type == "Customer" && (url.Contains("/ADMINISTRATOR/") || url.Contains("/MANAGE/"))) {
                context.Response.Redirect("/Home/Index");
                return;
            }
            

            await _next(context);
        }
    }
}
