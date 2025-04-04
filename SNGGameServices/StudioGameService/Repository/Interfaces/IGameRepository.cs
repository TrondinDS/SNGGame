<<<<<<< Updated upstream
﻿using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game, int> 
    {
        public IEnumerable<Game> GetFilterGame(ParamQuerySG paramQuerySG);
    }
}
=======
﻿using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game, int> 
    {
        public IEnumerable<Game> GetFilterGame(ParamQuerySG paramQuerySG);
    }
}
>>>>>>> Stashed changes
