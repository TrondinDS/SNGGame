using Library.Generics.GenericRepository.Interfaces;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameLibraryRepository : IGenericRepository<GameLibrary, Guid> 
    {}
}
