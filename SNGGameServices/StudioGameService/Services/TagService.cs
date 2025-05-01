using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace TagGameService.Services
{
    public class TagService : ITagService
    {
        private protected ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task AddAsync(Tag tag)
        {
            await tagRepository.AddAsync(tag);
            await tagRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tag = await tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                tagRepository.DeleteAsync(tag);
                await tagRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            return tagRepository.GetAllAsync();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await tagRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Tag tag)
        {
            tagRepository.UpdateAsync(tag);
            await tagRepository.SaveChangesAsync();
        }
    }
}
