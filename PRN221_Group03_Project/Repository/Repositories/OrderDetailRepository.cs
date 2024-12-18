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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private OrderDetailDAO _DAO;
        public OrderDetailRepository(OrderDetailDAO item)
        {
            _DAO = item;
        }
        public async Task<IEnumerable<OrderDetail>> GetListAll() => await _DAO.GetListAll();
        public async Task<OrderDetail> GetById(string id) => await _DAO.GetById(id);
        public async Task Add(OrderDetail item) => await _DAO.Add(item);
        public async Task Update(OrderDetail item) => await _DAO.Update(item); 
        public async Task Delete(string id) => await _DAO.Delete(id);
    }
}
