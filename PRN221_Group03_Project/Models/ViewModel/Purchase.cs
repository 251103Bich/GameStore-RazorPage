using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class Purchase
    {
        public string OID { get; set; }

        public string GameName {  get; set; }

        public long Price { get; set; }

        public DateTime Date { get; set; }
    }
}
