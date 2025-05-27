using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using OrganizerEventService.Services.Interfaces;

namespace OrganizerEventService.Services
{
    public class OrganizerService : IOrganizerService
    {
        public Task AddAsync(OrganizerDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizerDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GameDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrganizerDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
