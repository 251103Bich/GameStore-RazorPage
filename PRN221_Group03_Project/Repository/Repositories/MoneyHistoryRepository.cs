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
    public class MoneyHistoryRepository : IMoneyHistory
    {
        private MoneyHistoryDAO _DAO;
        public MoneyHistoryRepository(MoneyHistoryDAO item)
        {
            _DAO = item;
        }

        public async Task Add(MoneyHistory item) => await _DAO.Add(item);
        public async Task<IEnumerable<MoneyHistory>> GetByUsername(string user) => await _DAO.GetByUsername(user);
        public async Task<MoneyHistory> GetById(string id) => await _DAO.GetById(id);
        public async Task Update(MoneyHistory item) => await _DAO.Update(item);
    }
}
