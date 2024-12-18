using BussinessLogic.Interfaces;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
    public class MoneyManagementSevice : IMoneyManagementSevice
    {
        private readonly IMoneyManagementRepository _repo;

        public MoneyManagementSevice(IMoneyManagementRepository item)
        {
            _repo = item;
        }
        public async Task Add(MoneyManagement item) 
        { 
            await _repo.Add(item); 
        }

        public async Task<IEnumerable<(DateTime date, long revenue)>> GetTimeRevenue(string sellerName)
        {
            return await _repo.GetTimeRevenue(sellerName);
        }

        public async Task<IEnumerable<(string ID, DateTime date, string cusName, long gamePrice, long sellerPlusMoney)>> GetGameRevenue(string sellerName, string gameId)
        {
            return await _repo.GetGameRevenue(sellerName, gameId);
        }

        public async Task<IEnumerable<(string ID, string GID, DateTime date, long sellerMoney, long plusMoney)>> GetWebTotalRevene()
        {
            return await _repo.GetWebTotalRevene();
        }
    }
}
