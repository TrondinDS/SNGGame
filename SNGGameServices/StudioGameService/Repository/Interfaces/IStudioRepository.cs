﻿using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IStudioRepository : IGenericRepository<Studio, Guid> 
    {
        public Task<IEnumerable<Studio>> GetFiltreCardStudioAsync(ParamQueryStudio paramQueryListStudio);
        public Task<IEnumerable<Studio>> GetStudioByUserIdAsync(Guid id);
    }
}
