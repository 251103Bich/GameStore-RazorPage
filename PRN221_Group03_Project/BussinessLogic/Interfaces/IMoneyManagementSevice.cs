using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IMoneyManagementSevice
    {
        Task Add(MoneyManagement item);
        Task<IEnumerable<(DateTime date, long revenue)>> GetTimeRevenue(string sellerName);
        Task<IEnumerable<(string ID, DateTime date, string cusName, long gamePrice, long sellerPlusMoney)>> GetGameRevenue(string sellerName, string gameId);
        Task<IEnumerable<(string ID, string GID, DateTime date, long sellerMoney, long plusMoney)>> GetWebTotalRevene();
    }
}
