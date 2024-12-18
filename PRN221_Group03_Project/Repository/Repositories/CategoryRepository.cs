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
	public class CategoryRepository : ICategoryRepository
	{
		private CategoryDAO _DAO;
		public CategoryRepository(CategoryDAO item)
		{
			_DAO = item;
		}
		public async Task<IEnumerable<Category>> GetListAll() => await _DAO.GetListAll();
		public async Task<Category> GetById(string id) => await _DAO.GetById(id);

		public async Task<IEnumerable<Category>> GetCategoriesByGameId(string id) => await _DAO.GetCategoriesByGameId(id);

    }
}
