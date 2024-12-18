using DataAccess.DAOs;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MoneyManagementRepository : IMoneyManagementRepository
    {
        private MoneyManagementDAO _DAO;
        public MoneyManagementRepository(MoneyManagementDAO item)
        {
            _DAO = item;
        }
        public async Task Add(MoneyManagement item) => await _DAO.Add(item);
        public async Task<IEnumerable<(DateTime date, long revenue)>> GetTimeRevenue(string sellerName) => await _DAO.GetTimeRevenue(sellerName);
        public async Task<IEnumerable<(string ID, DateTime date, string cusName, long gamePrice, long sellerPlusMoney)>> GetGameRevenue(string sellerName, string gameId) => await _DAO.GetGameRevenue(sellerName, gameId);
        public async Task<IEnumerable<(string ID, string GID, DateTime date, long sellerMoney, long plusMoney)>> GetWebTotalRevene() => await _DAO.GetWebTotalRevene();

    }
}
