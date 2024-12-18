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
    public class OrderDetailSevice : IOrderDetailSevice
    {
        private readonly IOrderDetailRepository _repo;

        public OrderDetailSevice(IOrderDetailRepository item)
        {
            _repo = item;
        }
        public async Task<IEnumerable<OrderDetail>> GetListAll()
        {
            return await _repo.GetListAll();
        }
        public async Task<OrderDetail> GetById(string id)
        {
            return await _repo.GetById(id);
        }
        public async Task Add(OrderDetail item)
        {
            await _repo.Add(item);
        }
        public async Task Update(OrderDetail item)
        {
            await _repo.Update(item);
        }
        public async Task Delete(string id)
        {
            await _repo.Delete(id);
        }
    }
}
