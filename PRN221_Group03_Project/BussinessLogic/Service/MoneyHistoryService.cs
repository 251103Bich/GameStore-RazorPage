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
    public class MoneyHistoryService : IMoneyHistoryService
    {
        private readonly IMoneyHistory _repo;

        public MoneyHistoryService(IMoneyHistory item)
        {
            _repo = item;
        }
        public async Task Add(MoneyHistory item)
        {
            await _repo.Add(item);
        }
        public async Task<IEnumerable<MoneyHistory>> GetByUsername(string user)
        {
            return await _repo.GetByUsername(user);
        }
        public async Task<MoneyHistory> GetById(string id)
        {
            return await _repo.GetById(id);
        }
        public async Task Update(MoneyHistory item)
        {
            await _repo.Update(item);
        }
    }
}
