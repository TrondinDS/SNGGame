﻿using Library.Generics.GenericRepository;
using UserService.DB.Context;
using UserService.DB.Models;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class UserSubscriptionRepository : GenericRepository<UserSubscription, Guid>, IUserSubscriptionRepository
    {
        public UserSubscriptionRepository(ApplicationContext context)
            : base(context) { }
    }
}
