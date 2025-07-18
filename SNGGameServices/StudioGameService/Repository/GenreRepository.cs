﻿using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GenreRepository : GenericRepository<Genre, Guid>, IGenreRepository
    {
        public GenreRepository(ApplicationContext context)
            : base(context) { }
    }
}
