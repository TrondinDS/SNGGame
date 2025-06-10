using AdministratumService.DB.Models;
using AdministratumService.Repository.Interfaces;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;

namespace AdministratumService.Services
{
    public class ComplainTicketService : IComplainTicketService
    {
        private readonly IComplainTicketRepository repository;
        private readonly IMapper mapper;

        public ComplainTicketService(IComplainTicketRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddAsync(ComplainTicketDTO dto)
        {
            var model = mapper.Map<ComplainTicket>(dto);

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

        public async Task<IEnumerable<ComplainTicketDTO>> GetAllAsync()
        {
            var games = await repository.GetAllAsync();

            if (!games.Any())
                return Enumerable.Empty<ComplainTicketDTO>();

            var tasks = games.Select(model => ModelToDTOAsync(model));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<ComplainTicketDTO> ModelToDTOAsync(ComplainTicket model)
        {
            if (model == null) return null;
            try
            {
                return mapper.Map<ComplainTicketDTO>(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке тикета с ID {model.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<ComplainTicketDTO> GetByIdAsync(Guid id)
        {
            var model = await repository.GetByIdAsync(id);
            if (model == null) return null;
            return await ModelToDTOAsync(model);
        }

        public async Task UpdateAsync(ComplainTicketDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            await repository.BeginTransactionAsync();

            try
            {
                var existingModel = await repository.GetByIdAsync(model.Id);
                if (existingModel == null)
                    throw new KeyNotFoundException($"Тикет с ID {model.Id} не найден.");

                mapper.Map(model, existingModel);
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
    }
}
