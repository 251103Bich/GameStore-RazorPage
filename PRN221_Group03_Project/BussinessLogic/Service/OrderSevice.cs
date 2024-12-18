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
    public class OrderSevice : IOrderSevice
    {
        private readonly IOrderRepository _repo;

        public OrderSevice(IOrderRepository item)
        {
            _repo = item;
        }
        public async Task<IEnumerable<Order>> GetListAll()
        {
            return await _repo.GetListAll();
        }
        public async Task<Order> GetById(string id)
        {
            return await _repo.GetById(id);
        }
        public async Task Add(Order item)
        {
            await _repo.Add(item);
        }
        public async Task Update(Order item)
        {
            await _repo.Update(item);
        }
        public async Task Delete(string id)
        {
            await _repo.Delete(id);
        }
    }
}
