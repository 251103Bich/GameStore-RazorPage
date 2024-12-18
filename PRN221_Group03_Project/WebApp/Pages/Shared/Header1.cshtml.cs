using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using BussinessLogic.Interfaces;

namespace WebApp.Pages.Shared
{
    public class Header1Model : PageModel
    {
        /*  public string Username { get; set; } // Use PascalCase for public properties*/

        public void OnGet()
        {
            /*// Attempt to get the username from the session first
            Username = HttpContext.Session.GetString("Username");

            // If the session username is not available, check the cookies
            if (string.IsNullOrEmpty(Username) && Request.Cookies.ContainsKey("Username"))
            {
                Username = Request.Cookies["Username"];
            }*/

        }
    }
}
