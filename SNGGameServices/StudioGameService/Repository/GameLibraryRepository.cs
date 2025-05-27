using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameLibraryRepository : GenericRepository<GameLibrary, int>, IGameLibraryRepository
    {
        private DbSet<GameLibrary> dbSetGameLibrary;
        private DbSet<Game> dbSetGame;
        private DbSet<StatisticGame> dbSetStatisticGame;
        public GameLibraryRepository(ApplicationContext context)
            : base(context) 
        {
            dbSetGameLibrary = context.Set<GameLibrary>();
            dbSetGame = context.Set<Game>();
            dbSetStatisticGame = context.Set<StatisticGame>();
        }

        public override async Task AddAsync(params GameLibrary[] gls)
        {
            foreach (var gameLibrary in gls)
            {
                var userGame = await dbSetGameLibrary
                    .FirstOrDefaultAsync(gl => gl.UserId == gameLibrary.UserId && gl.GameId == gameLibrary.GameId);

                if (userGame != null)
                {
                    throw new ArgumentException($"Пользователь {gameLibrary.UserId} уже добавил эту игру в свою библиотеку.");
                }

                var game = await dbSetGame
                    .Include(g => g.StatisticGame)
                    .FirstOrDefaultAsync(g => g.Id == gameLibrary.GameId);

                if (game == null)
                {
                    throw new ArgumentException($"Игра с ID {gameLibrary.GameId} не найдена.");
                }

                if (game.StatisticGame == null)
                {
                    game.StatisticGame = new StatisticGame
                    {
                        RatingSum = gameLibrary.Rating,
                        PeopleCount = 1,
                        GameId = game.Id
                    };

                    await dbSetStatisticGame.AddRangeAsync(game.StatisticGame);
                }
                else
                {
                    game.StatisticGame.RatingSum += gameLibrary.Rating;
                    game.StatisticGame.PeopleCount++;
                }

                await dbSetGameLibrary.AddRangeAsync(gameLibrary);
            }
        }

        public override async Task UpdateAsync(params GameLibrary[] gls)
        {
            foreach (var gameLibrary in gls)
            {
                var gameLibraryFromDb = await dbSetGameLibrary
                                        .Include(gl => gl.Game)
                                        .ThenInclude(g => g.StatisticGame)
                                        .FirstOrDefaultAsync(gl => gl.Id == gameLibrary.Id);

                if (gameLibraryFromDb == null)
                {
                    throw new ArgumentException($"Отзыв с ID {gameLibrary.Id} не найден.");
                }

                if (gameLibraryFromDb.GameId != gameLibrary.GameId)
                {
                    throw new ArgumentException($"Не совпадают Id игр");
                }

                if (gameLibraryFromDb.Game == null)
                {
                    throw new ArgumentException($"Игра с ID {gameLibrary.GameId} не найдена.");
                }
                else
                {
                    gameLibraryFromDb.Game.StatisticGame.RatingSum += gameLibrary.Rating - gameLibraryFromDb.Rating;

                    gameLibraryFromDb.Rating = gameLibrary.Rating;
                    gameLibraryFromDb.Status = gameLibrary.Status;
                    gameLibraryFromDb.IsBought = gameLibrary.IsBought;
                    gameLibraryFromDb.Date = gameLibrary.Date;
                }
            }
        }

        public override async Task DeleteAsync(params GameLibrary[] gls)
        {
            foreach (var gameLibrary in gls)
            {
                // Находим запись в базе данных
                var existingGameLibrary = await dbSetGameLibrary
                    .Include(gl => gl.Game)
                    .ThenInclude(g => g.StatisticGame)
                    .FirstOrDefaultAsync(gl => gl.Id == gameLibrary.Id);

                if (existingGameLibrary == null)
                {
                    throw new ArgumentException($"Запись GameLibrary с ID {gameLibrary.Id} не найдена.");
                }

                if (existingGameLibrary.Game?.StatisticGame != null)
                {
                    var statisticGame = existingGameLibrary.Game.StatisticGame;

                    // Корректируем сумму рейтингов и количество людей
                    statisticGame.RatingSum -= existingGameLibrary.Rating;
                    statisticGame.PeopleCount--;

                }

                // Удаляем запись из базы данных
                dbSetGameLibrary.RemoveRange(existingGameLibrary);
            }
        }
    }
}
