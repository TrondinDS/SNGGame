using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;

namespace AdministratumService.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository repository;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddAsync(MessageDTO dto)
        {
            var model = mapper.Map<Message>(dto);

            try
            {
                await repository.BeginTransactionAsync(); // Начинаем транзакцию

                await repository.AddAsync(model);
                await repository.SaveChangesAsync(); // Сохраняем в рамках транзакции
                await repository.CommitTransactionAsync(); // Фиксируем изменения
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync(); // Откатываем Postgres
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await repository.GetByIdAsync(id);
            if (model == null) return;

            try
            {
                await repository.BeginTransactionAsync();

                await repository.DeleteAsync(model);
                await repository.SaveChangesAsync();
                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var games = await repository.GetAllAsync();

            if (!games.Any())
                return Enumerable.Empty<MessageDTO>();

            var tasks = games.Select(model => ModelToDTOAsync(model));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<MessageDTO> ModelToDTOAsync(Message model)
        {
            if (model == null) return null;
            try
            {
                return mapper.Map<MessageDTO>(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке тикета с ID {model.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<MessageDTO> GetByIdAsync(Guid id)
        {
            var model = await repository.GetByIdAsync(id);
            if (model == null) return null;
            return await ModelToDTOAsync(model);
        }

        public async Task UpdateAsync(MessageDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await repository.BeginTransactionAsync();

            try
            {
                var existingModel = await repository.GetByIdAsync(dto.Id);
                if (existingModel == null)
                    throw new KeyNotFoundException($"Тикет с ID {dto.Id} не найден.");

                mapper.Map(dto, existingModel);
                await repository.UpdateAsync(existingModel);
                await repository.SaveChangesAsync();
                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка при обновлении тикета: {ex.Message}");
                throw;
            }
        }

        private async Task<IEnumerable<MessageDTO>> ModelToDTOs(IEnumerable<Message> elems)
        {
            if (!elems.Any())
                return Enumerable.Empty<MessageDTO>();

            var tasks = elems.Select(ModelToDTOAsync);
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<IEnumerable<MessageDTO>> Filter(ParamQueryMessage param)
        {
            var elems = await repository.Filter(param);
            return await ModelToDTOs(elems);
        }
    }
}
