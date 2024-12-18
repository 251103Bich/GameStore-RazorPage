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
	public class GameRepository : IGameRepository
	{
		private GameDAO _DAO;
		private FeedbackDAO _FeedbackDAO;
		public GameRepository(GameDAO item, FeedbackDAO item2)
		{
			_DAO = item;
			_FeedbackDAO = item2;
		}
		public async Task<IEnumerable<Game>> GetListAll() => await _DAO.GetListAll();
		public async Task<IEnumerable<Game>> GetTopSaleGameList() => await _DAO.GetTopSaleGameList();
        public async Task<IEnumerable<Game>> GetNewGameList() => await _DAO.GetNewGameList();
		public async Task<IEnumerable<Game>> GetGamesBySellerName(string sellerName) => await _DAO.GetGamesBySellerName(sellerName);
		public async Task<IEnumerable<Game>> GetGamesByGameName(string name) => await _DAO.GetGamesByGameName(name);
        public async Task<Game> GetById(string id) => await _DAO.GetById(id);
        public async Task<List<Category>> GetCategoryByGameId(string item) => await _DAO.GetCategoryByGameId(item);
        public async Task Add(Game item, List<string> item2) => await _DAO.Add(item, item2);
        public async Task Update(Game item, List<string> item2) => await _DAO.Update(item, item2);
        public async Task Delete(string id) => await _DAO.Delete(id);
		public async Task<IEnumerable<Game>> searchGameByCategory(string id) => await _DAO.searchGameByCategory(id);
        public async Task AddCard(string gid, string uid) => await _DAO.AddCard(gid, uid);
		public async Task<IEnumerable<Game>> GetCard(string uid) => await _DAO.GetCard(uid);
		public async Task DeleteCard(string gid, string uid) => await _DAO.DeleteCard(gid, uid);
		public async Task<IEnumerable<Feedback>> GetAllFeedbackByGID(string gid) => await _FeedbackDAO.GetAllFeedbackByGID(gid);
        public async Task<IEnumerable<Game>> GetMyGames(Profile item) => await _DAO.GetMyGames(item);
		public async Task<bool> CheckCart(string uid, string gid) => await _DAO.CheckCart(uid, gid);

		public async Task<IEnumerable<Game>> GetGameAfterDelete() => await _DAO.GetGameDelete();

        public async Task<Game> ApproveGame(string username) => await _DAO.ApproveGame(username);
        public async Task<IEnumerable<Game>> GetGameApproveAll() => await _DAO.GetGameApproveAll();
    }
}
