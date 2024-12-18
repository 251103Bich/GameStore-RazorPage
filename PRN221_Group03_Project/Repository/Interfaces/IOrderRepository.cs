using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetListAll();
        Task<Order> GetById(string id);
        Task Add(Order item);
        Task Update(Order item);
        Task Delete(string id);

    }
}
