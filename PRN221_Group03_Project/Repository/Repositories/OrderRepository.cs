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
    public class OrderRepository : IOrderRepository
    {
        private OrderDAO _DAO;
        public OrderRepository(OrderDAO item)
        {
            _DAO = item;
        }
        public async Task<IEnumerable<Order>> GetListAll() => await _DAO.GetListAll();
        public async Task<Order> GetById(string id) => await _DAO.GetById(id);
        public async Task Add(Order item) => await _DAO.Add(item);
        public async Task Update(Order item) => await _DAO.Update(item);
        public async Task Delete(string id) => await _DAO.Delete(id);
    }
}
