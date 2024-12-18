using BussinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Models;
using WebApp.Hubs;

namespace WebApp.Pages.ShoppingCard
{
    public class IndexModel : PageModel
    {
        private readonly IProfileService _profileService;
        private readonly IGameService _gameService;
        private readonly IOrderDetailSevice _orderDetailSevice;
        private readonly IOrderSevice _orderSevice;
        private readonly IMoneyManagementSevice _moneyManagementSevice;
        private readonly IHubContext<SignalrHub> _hubContext;

        public IndexModel(IProfileService profileService, IGameService gameService, IHubContext<SignalrHub> hubContext, IOrderDetailSevice orderDetailSevice, IOrderSevice orderSevice, IMoneyManagementSevice moneyManagementSevice)
        {
            _profileService = profileService;
            _gameService = gameService;
            _hubContext = hubContext;
            _orderDetailSevice = orderDetailSevice;
            _orderSevice = orderSevice;
            _moneyManagementSevice = moneyManagementSevice;
        }

        [BindProperty]
        public Models.Profile _Profile { get; set; }
        public string ErrorMessage { get; set; }
        string item3 = "user01";
        [BindProperty]
        public string item2 { get; set; }
        public IEnumerable<Models.Game> listCard;

        public async Task OnGetAsync()
        {
            string item = Request.Cookies["Username"];
            listCard = await _gameService.GetCard(item);
        }

        public async Task<RedirectToPageResult> OnPostAsync(string id)
        {
            string item = Request.Cookies["Username"];
            await _profileService.DeleteCard(id, item);
            return RedirectToPage("/ShoppingCard/Index");
        }
        public async Task<IActionResult> OnPostPayAsync(decimal txtTotalMoney)
        {
            string item = Request.Cookies["Username"];
            Models.Profile profile = await _profileService.GetProfileByUsername(item);
            if (profile.Money < txtTotalMoney)
            {
                ErrorMessage = "Your account don't enough money!";
                await OnGetAsync();
                return Page();
            }
            else
            {
                profile.Money = (long)(profile.Money - txtTotalMoney);
                await _profileService.UpdateProfile(profile);
                Order od = new Order();
                od.Oid = item + DateTime.Now;
                od.Username = item;
                od.Total = (long)txtTotalMoney;
                od.Date = DateTime.Now;
                await _orderSevice.Add(od);
                listCard = await _gameService.GetCard(item);

                var cardListCopy = listCard.ToList();
                foreach (var game in cardListCopy)
                {
                    OrderDetail odd = new OrderDetail();
                    MoneyManagement moneyManagement = new MoneyManagement();
                    odd.Oid = od.Oid;
                    odd.Odid = item + game.GameName + DateTime.Now;
                    // Giới hạn độ dài xuống còn 50 ký tự
                    if (odd.Odid.Length > 50)
                    {
                        odd.Odid = odd.Odid.Substring(0, 50); // Rút ngắn xuống 50 ký tự
                    }
                    odd.Gid = game.Gid;
                    odd.Price = game.Price;
                    await _orderDetailSevice.Add(odd);
                    moneyManagement.Odid = odd.Odid;
                    moneyManagement.Username = item;
                    moneyManagement.Admin = "user01";
                    moneyManagement.Admoney = (long)(game.Price * 0.1);
                    moneyManagement.SellerMoney = (long)(game.Price * 0.9);
                    moneyManagement.Date = DateTime.Now;
                    await _moneyManagementSevice.Add(moneyManagement);
                    Models.Profile seller = await _profileService.GetProfileByUsername(game.SellerName);
                    await _profileService.UpdateProfile(seller);
                    Models.Profile admin = await _profileService.GetProfileByUsername("user01");
                    await _profileService.UpdateProfile(admin);
                    await _gameService.DeleteCard(game.Gid, item);
                }

            }

            // Optionally, store user information in cookies
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Set cookie to expire in 7 days
                HttpOnly = true, // Prevents client-side JavaScript from accessing the cookie
                Secure = true // Ensures cookie is sent only over HTTPS
            };

            Response.Cookies.Append("Money", profile.Money.ToString(), cookieOptions);

            return Page();
        }
    }
}
