using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Library.Generics.Interceptors
{
    public class InterceptorOverrideDelete : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData dbEvent, InterceptionResult<int> result)
        {
            if (dbEvent.Context is null) return result;

            foreach (var item in dbEvent.Context.ChangeTracker.Entries())
            {
                if (item is not { State: EntityState.Deleted, Entity: IIsDeleted delete }) continue;
                item.State = EntityState.Modified;
                delete.IsDeleted = true;
                delete.DateDeleted = DateTime.UtcNow;
            }
            return result;
        }

        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData dbEvent, InterceptionResult<int> result, CancellationToken token = default)
        {
            if (dbEvent.Context is null) return await Task.FromResult(result);

            foreach (var item in dbEvent.Context.ChangeTracker.Entries())
            {
                if (item is not { State: EntityState.Deleted, Entity: IIsDeleted delete }) continue;
                item.State = EntityState.Modified;
                delete.IsDeleted = true;
                delete.DateDeleted = DateTime.UtcNow;
            }
            return await Task.FromResult(result);
        }
    }
}
