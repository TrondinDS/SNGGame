﻿using Library.Generics.GenericRepository.Interfaces;
using UserActivityService.DB.Models;

namespace UserActivityService.Repository.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment, Guid>
    {
    }
}
