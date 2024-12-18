using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Home
{
    public class SuccessModel : PageModel
    {
        public string successMessage {  get; set; }
        public void OnGet()
        {
            if(TempData["SuccessMessage"] != null)
            {
                successMessage = TempData["SuccessMessage"] as string;
            }
            TempData.Clear();
        }
    }
}
