using AutoMapper;
using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelObjects.Studio;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly IStudioService studioService;
        private readonly IMapper mapper;

        public CardController(IGameService gameService, IStudioService studioService, IMapper mapper)
        {
            this.gameService = gameService;
            this.studioService = studioService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех карточек игр
        /// </summary>
        /// <returns>Список всех карточек игр</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardGameDTO>>> GetAllCardGames()
        {
            var games = await gameService.GetAllCardGameAsync();
            var gameCards = mapper.Map<IEnumerable<CardGameDTO>>(games);
            return Ok(gameCards);
        }

        /// <summary>
        /// Получение выбранных карточек игр
        /// </summary>
        /// <returns>Список выбранных карточек игр</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CardGameDTO>>> GetSelectCardGames(List<int> IdGames)
        {
            var games = await gameService.GetSelectCardGameAsync(IdGames);
            var gameCards = mapper.Map<IEnumerable<CardGameDTO>>(games);
            return Ok(gameCards);
        }

        /// <summary>
        /// Получение карточек игр с помощью фильтра
        /// </summary>
        /// <returns>Список карточек игр с помощью фильтра</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CardGameDTO>>> GetFiltreCardGames(ParamQuerySG paramQuery)
        {
            var games = await gameService.GetFiltreCardGameAsync(paramQuery);
            var gameCards = mapper.Map<IEnumerable<CardGameDTO>>(games);
            return Ok(gameCards);
        }

        /// <summary>
        /// Получение карточек студий по списку id
        /// </summary>
        /// <returns>Список карточек студий по списку id</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CardStudioDTO>>> GetSearchCardStudio(QueryListStudio paramQueryListStudio)
        {
            var studio = await studioService.GetFiltreCardStudioAsync(paramQueryListStudio);
            var studioCards = mapper.Map<IEnumerable<CardStudioDTO>>(studio);
            return Ok(studioCards);
        }
    }
}
