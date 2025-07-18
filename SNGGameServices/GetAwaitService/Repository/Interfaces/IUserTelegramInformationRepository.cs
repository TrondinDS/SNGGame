﻿using GetAwaitService.DB.Models;
using Library.Generics.GenericRepository.Interfaces;

namespace GetAwaitService.Repository
{
    public interface IUserTelegramInformationRepository : IGenericRepository<UserTelegramInformation, Guid>
    {
        public Task<UserTelegramInformation> GetUserTgInfoFromTgId(ulong tgId);
        public Task<UserTelegramInformation> GetUserByIdUser(Guid userId);
    }
}
