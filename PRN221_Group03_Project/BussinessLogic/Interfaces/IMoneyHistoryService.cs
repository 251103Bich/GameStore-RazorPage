using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IMoneyHistoryService
    {
        Task Add(MoneyHistory item);
        Task<IEnumerable<MoneyHistory>> GetByUsername(string user);
        Task<MoneyHistory> GetById(string id);
        Task Update(MoneyHistory item);
    }
}
