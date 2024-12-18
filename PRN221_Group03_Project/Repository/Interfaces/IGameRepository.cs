using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IGameRepository
	{
		Task<IEnumerable<Game>> GetListAll();
		Task<IEnumerable<Game>> GetTopSaleGameList();
		Task<IEnumerable<Game>> GetNewGameList();
		Task<IEnumerable<Game>> GetGamesBySellerName(string sellerName);
		Task<IEnumerable<Game>> GetGamesByGameName(string name);
        Task<Game> GetById(string id);
        Task<List<Category>> GetCategoryByGameId(string item);
        Task Add(Game item, List<string> item2);
        Task Update(Game item, List<string> item2);
        Task Delete(string id);
        Task<IEnumerable<Game>> searchGameByCategory(string id);
        Task AddCard(string gid, string uid);
        Task<IEnumerable<Game>> GetCard(string uid);
        Task DeleteCard(string gid, string uid);
        Task<IEnumerable<Feedback>> GetAllFeedbackByGID(string gid);
        Task<IEnumerable<Game>> GetMyGames(Profile item);
        Task<bool> CheckCart(string uid, string gid);
        Task<IEnumerable<Game>> GetGameAfterDelete();

        Task<Game> ApproveGame(string username);
        Task<IEnumerable<Game>> GetGameApproveAll();
    }
}
