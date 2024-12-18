using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface IOrderDetailSevice
    {
        Task<IEnumerable<OrderDetail>> GetListAll();
        Task<OrderDetail> GetById(string id);
        Task Add(OrderDetail item);
        Task Update(OrderDetail item);
        Task Delete(string id);
    }
}
