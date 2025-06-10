using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUserApiService _userApiService;

        public IndexModel(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public List<UserDTO> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _userApiService.GetAllUsersAsync();
            Users = result?.ToList() ?? new();
        }
    }
}
