using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels
{
    public class SessionService
    {
        public GameViewModel GameViewModel { get; set; }
        public ProfileSellerViewModel ProfileSellerViewModel { get; set; }

        public SessionService(GameViewModel gameViewModel, ProfileSellerViewModel profileSellerViewModel)
        {
            GameViewModel = gameViewModel;
            ProfileSellerViewModel = profileSellerViewModel;
        }
    }

}
