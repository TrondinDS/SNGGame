﻿using Library.Generics.GenericRepository.Interfaces;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IStatisticGameRepository : IGenericRepository<StatisticGame, Guid> 
    {
    }
}
