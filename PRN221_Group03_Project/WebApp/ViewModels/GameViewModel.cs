using Models;

namespace WebApp.ViewModels
{
    public class GameViewModel
    {
        public string Gid { get; set; }

        public string GameName { get; set; }

        public long Price { get; set; }

        public string Picture { get; set; }

        public DateTime Date { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Description { get; set; }

        public string Configuration { get; set; }

        public string SellerName { get; set; }

        public string Status { get; set; }

        public string DownloadFile { get; set; }
    }
}
