using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Library.Services;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository.Interfaces;
using OrganizerEventService.Services.Interfaces;

namespace OrganizerEventService.Services
{
    public class EventService : IEventService
    {
        protected readonly IEventRepository repository;
        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        const string imgsDatabase = "ImagesDatabase";
        const string eventImgsCollection = "EventImagesCollection";

        const string contentDatabase = "EventContentDatabase";
        const string contentCollection = "EventContentCollection";

        public EventService(IEventRepository repository, Mongo mongoService, IMapper mapper)
        {
            this.repository = repository;
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        public async Task AddAsync(EventDTO dto)
        {
            var model = mapper.Map<Event>(dto);
            try
            {
                await repository.BeginTransactionAsync();

                await repository.AddAsync(model);
                await repository.SaveChangesAsync();

                var imgTask = mongoService.Database(imgsDatabase)
                    .Collection(eventImgsCollection)
                    .InsertImg(model.Id, dto.Image, dto.ImageType);
                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(model.Id, dto.Content);

                await Task.WhenAll(imgTask, contentTask);

                await repository.CommitTransactionAsync();

                dto.Id = model.Id;
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                await CompensateAddAsync(model.Id);
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        private async Task CompensateAddAsync(Guid eventId)
        {
            try
            {
                var deleteImgTask = mongoService.Database(imgsDatabase)
                    .Collection(eventImgsCollection)
                    .Delete(eventId);
                var deleteContentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(eventId);

                await Task.WhenAll(deleteImgTask, deleteContentTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during compensation: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var eventItem = await repository.GetByIdAsync(id);
            if (eventItem == null) return;

            try
            {
                await repository.BeginTransactionAsync();

                var deleteTask = repository.DeleteAsync(eventItem);
                var saveTask = repository.SaveChangesAsync();
                var deleteImgTask = mongoService.Database(imgsDatabase)
                    .Collection(eventImgsCollection)
                    .Delete(id);
                var deleteContentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(id);

                await Task.WhenAll(deleteTask, saveTask, deleteImgTask, deleteContentTask);
                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EventDTO>> GetAllAsync()
        {
            var events = await repository.GetAllAsync();

            if (!events.Any())
                return Enumerable.Empty<EventDTO>();

            var tasks = events.Select(ev => ModelToDTO(ev));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<EventDTO> GetByIdAsync(Guid id)
        {
            var eventItem = await repository.GetByIdAsync(id);
            return eventItem == null ? null : await ModelToDTO(eventItem);
        }

        public async Task UpdateAsync(EventDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await repository.BeginTransactionAsync();
            try
            {
                var existingEvent = await repository.GetByIdAsync(dto.Id) ??
                    throw new KeyNotFoundException($"Event with ID {dto.Id} not found.");

                mapper.Map(dto, existingEvent);
                await repository.UpdateAsync(existingEvent);
                await repository.SaveChangesAsync();

                var imageTask = mongoService.Database(imgsDatabase).Collection(eventImgsCollection).UpdateImg(dto.Id, dto.Image, dto.ImageType);
                var contentTask = mongoService.Database(contentDatabase).Collection(contentCollection).UpdateStrContent(dto.Id, dto.Content);

                await Task.WhenAll(imageTask, contentTask);
                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Error updating event: {ex.Message}");
                throw;
            }
        }

        private async Task<EventDTO> ModelToDTO(Event eventItem)
        {
            if (eventItem == null) return null;

            try
            {
                var imageTask = mongoService.Database(imgsDatabase)
                    .Collection(eventImgsCollection)
                    .GetImgById(eventItem.Id);

                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .GetContentById(eventItem.Id);

                await Task.WhenAll(imageTask, contentTask);

                var dto = mapper.Map<EventDTO>(eventItem);
                dto.Image = imageTask.Result?.Bytes;
                dto.ImageType = imageTask.Result?.ContentType;
                dto.Content = contentTask.Result?.Value;

                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing event with ID {eventItem.Id}: {ex.Message}");
                return null;
            }
        }

        private async Task<IEnumerable<EventDTO>> ModelToDTOs(IEnumerable<Event> games)
        {
            if (!games.Any())
                return Enumerable.Empty<EventDTO>();

            var tasks = games.Select(ModelToDTO);
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<IEnumerable<EventDTO>> Filter(ParamQueryEvent param)
        {
            var elems = await repository.Filter(param);
            return await ModelToDTOs(elems);
        }
    }
}