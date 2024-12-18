using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetListAll();
        Task<Category> GetById(string id);
        Task<IEnumerable<Category>> GetCategoriesByGameId(string id);
    }
}
