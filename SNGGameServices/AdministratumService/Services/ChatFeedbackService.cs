using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Services
{
    public class ChatFeedbackService : IChatFeedbackService
    {
        private readonly IChatFeedbackRepository repository;
        private readonly IMapper mapper;

        public ChatFeedbackService(IChatFeedbackRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddAsync(ChatFeedbackDTO dto)
        {
            var model = mapper.Map<ChatFeedback>(dto);

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

        public async Task<IEnumerable<ChatFeedbackDTO>> GetAllAsync()
        {
            var games = await repository.GetAllAsync();

            if (!games.Any())
                return Enumerable.Empty<ChatFeedbackDTO>();

            var tasks = games.Select(model => ModelToDTOAsync(model));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<ChatFeedbackDTO> ModelToDTOAsync(ChatFeedback model)
        {
            if (model == null) return null;
            try
            {
                return mapper.Map<ChatFeedbackDTO>(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке чата с ID {model.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<ChatFeedbackDTO> GetByIdAsync(Guid id)
        {
            var model = await repository.GetByIdAsync(id);
            if (model == null) return null;
            return await ModelToDTOAsync(model);
        }

        public async Task UpdateAsync(ChatFeedbackDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            await repository.BeginTransactionAsync();

            try
            {
                var existingGame = await repository.GetByIdAsync(model.Id);
                if (existingGame == null)
                    throw new KeyNotFoundException($"Чата с ID {model.Id} не найдена.");

                mapper.Map(model, existingGame);
                await repository.UpdateAsync(existingGame);
                await repository.SaveChangesAsync();
                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка при обновлении чата: {ex.Message}");
                throw;
            }
        }

        private async Task<IEnumerable<ChatFeedbackDTO>> ModelToDTOs(IEnumerable<ChatFeedback> elems)
        {
            if (!elems.Any())
                return Enumerable.Empty<ChatFeedbackDTO>();

            var tasks = elems.Select(ModelToDTOAsync);
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<IEnumerable<ChatFeedbackDTO>> Filter(ParamQueryChatfeedback param)
        {
            var elems = await repository.Filter(param);
            return await ModelToDTOs(elems);
        }
    }
}
