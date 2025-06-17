using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Filter;
using StudioGameService.Repository.Interfaces;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudioGameService.Repository
{
    public class GameRepository : GenericRepository<Game, Guid>, IGameRepository
    {
        private readonly DbSet<Game> dbSetGame;
        private readonly DbSet<StatisticGame> dbSetStatisticGame;
        public GameRepository(ApplicationContext context)
            : base(context)
        {
            dbSetGame = context.Set<Game>();
            dbSetStatisticGame = context.Set<StatisticGame>();
        }

        public async Task<IEnumerable<Game>> GetFilterGame(ParamQueryGame paramQuerySG)
        {
            var query = dbSetGame.AsQueryable();

            query = FilterQueryGame.CreateQuerybleAsNoTraking(paramQuerySG, query);
            var result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Game>> GetAllCardGameAsync()
        {
            var result = await dbSetGame
                .Include(game => game.Studio)
                .Include(game => game.Genres)
                .ThenInclude(gsg => gsg.Genre)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames)
        {
            var result = await dbSetGame
                .Include(game => game.Studio)
                .Include(game => game.Genres)
                .ThenInclude(gsg => gsg.Genre)
                .Where(game => idGames.Contains(game.Id)) 
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
        
        public async Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG)
        {
            var query = dbSetGame
                .Include(game => game.Studio)
                .Include(game => game.Genres)
                .ThenInclude(gsg => gsg.Genre)
                .AsQueryable();

            query = FilterQueryGame.CreateQuerybleAsNoTraking(paramQuerySG, query);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId)
        {
            var listStatistic = await dbSetStatisticGame.Where(sg => listGameId.Contains(sg.GameId)).ToListAsync();
            return listStatistic;
        }

        public async Task<IEnumerable<Game>> GetGameDTOViewByIdGamesAsync(IEnumerable<Guid> listGameId)
        {
            return await dbSetGame
                .Include(g => g.StatisticGame)
                .Include(gsg => gsg.Genres)
                .ThenInclude(gsg => gsg.Genre)
                .Include(gst => gst.Tags)
                .ThenInclude(gst => gst.Tag)
                .Where(g => listGameId.Contains(g.Id))
                .ToListAsync();
        }
    }
}
