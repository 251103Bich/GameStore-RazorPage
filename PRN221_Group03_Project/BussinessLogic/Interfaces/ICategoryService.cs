using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetListAllCategory();
		Task<Category> GetCategoryById(string id);
		Task<IEnumerable<Category>> GetCategoriesByGameId(string id);

    }
}
