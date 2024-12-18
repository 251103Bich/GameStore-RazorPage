using BussinessLogic.Interfaces;
using Models;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _repo;
		private readonly IProfileRepository _profileRepository;

		public GameService(IGameRepository item, IProfileRepository item2)
		{
			_repo = item;
			_profileRepository = item2;
		}

		public async Task<IEnumerable<Game>> GetListAllGame()
		{
			return await _repo.GetListAll();
		}

		public async Task<IEnumerable<Game>> GetTopSaleGameList()
		{
			return await _repo.GetTopSaleGameList();
		}

		public async Task<IEnumerable<Game>> GetNewGameList()
		{
			return await _repo.GetNewGameList();
		}

		public async Task<IEnumerable<Game>> GetGamesBySellerName(string sellerName)
		{
			return await _repo.GetGamesBySellerName(sellerName);
		}

		public async Task<IEnumerable<Game>> GetGamesByGameName(string name)
		{
			return await _repo.GetGamesByGameName(name);
		}

        public async Task<Game> GetGameById(string id)
		{
			return await _repo.GetById(id);
        }

        public async Task<List<Category>> GetCategoryByGameId(string item)
        {
            return await _repo.GetCategoryByGameId(item);
        }

        public async Task AddGame(Game item, List<string> item2)
        {
            // Thêm logic nghiệp vụ nếu cần
            await _repo.Add(item, item2);
        }

        public async Task UpdateGame(Game item, List<string> item2)
        {
            // Thêm logic nghiệp vụ nếu cần
            await _repo.Update(item, item2);
        }

        public async Task DeleteGame(string id)
		{
			// Thêm logic nghiệp vụ nếu cần
			await _repo.Delete(id);
		}
		public async Task<IEnumerable<Game>> searchGameByCategory(string id)
		{
			return await _repo.searchGameByCategory(id);
		}
		public async Task AddCard(string gid, string uid)
		{
			var profile = await _profileRepository.GetById(uid);
			if (profile.GidsNavigation.Any(g => g.Gid == gid))
			{
				await _profileRepository.UpdateWishlist(profile, gid);
			}
			await _repo.AddCard(gid, uid);
		}
		public async Task<IEnumerable<Game>> GetCard(string uid)
		{
            return await _repo.GetCard(uid);
		}
		public async Task DeleteCard(string gid, string uid)
		{
			await _repo.DeleteCard(gid, uid);
		}
		public async Task<IEnumerable<Feedback>> GetAllFeedbackByGID(string gid) => await _repo.GetAllFeedbackByGID(gid);
        public async Task<IEnumerable<Game>> GetMyOwnGames(Profile item) => await _repo.GetMyGames(item);
		public async Task<bool> CheckCart(string uid, string gid)
		{
			return await _repo.CheckCart(uid, gid);
		}

		public async Task<IEnumerable<Game>> GetGameAfterDelete() => await _repo.GetGameAfterDelete();
        public async Task<Game> ApproveGame(string username) => await _repo.ApproveGame(username);
        public async Task<IEnumerable<Game>> GetGameApproveAll() => await _repo.GetGameApproveAll();

    }
}
