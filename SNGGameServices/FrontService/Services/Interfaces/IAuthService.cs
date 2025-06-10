namespace FrontService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> GetJwtTokenAsync(long telegramId);
    }
}
