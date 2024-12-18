using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Home
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();
            return RedirectToPage("/Home/Index");
        }
    }
}
