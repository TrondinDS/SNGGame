using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Studio
{
    public class EditModel : PageModel
    {
        private readonly IStudioApiService _studioApiService;

        public EditModel(IStudioApiService studioApiService)
        {
            _studioApiService = studioApiService;
        }

        public StudioDTO Studio { get; set; }

        [BindProperty]
        public StudioDTO StudioNew { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var studio = await _studioApiService.GetStudioByIdAsync(id);
            if (studio == null)
                return NotFound();

            Studio = studio;

            StudioNew = new StudioDTO
            {
                Id = studio.Id,
                Title = studio.Title,
                Email = studio.Email,
                IsResolutionPublication = studio.IsResolutionPublication,
                CreatorId = studio.CreatorId,
                OwnerId = studio.OwnerId,
                Image = studio.Image,
                ImageType = studio.ImageType,
                Content = studio.Content
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var studio = await _studioApiService.GetStudioByIdAsync(StudioNew.Id);
            if (studio == null)
            {
                TempData["ErrorMessage"] = "Студия не найдена.";
                return RedirectToPage();
            }

            studio.Title = StudioNew.Title;
            studio.Email = StudioNew.Email;
            studio.IsResolutionPublication = StudioNew.IsResolutionPublication;
            studio.CreatorId = StudioNew.CreatorId;
            studio.OwnerId = StudioNew.OwnerId;
            studio.Content = StudioNew.Content;

            if (ImageFile is { Length: > 0 })
            {
                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(ImageFile.ContentType))
                {
                    TempData["ErrorMessage"] = "Разрешены только JPG и PNG.";
                    return RedirectToPage();
                }

                using var memoryStream = new MemoryStream();
                await ImageFile.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                studio.Image = Convert.ToBase64String(fileBytes);
                studio.ImageType = ImageFile.ContentType;
            }

            var success = await _studioApiService.UpdateStudioAsync(studio);
            if (!success)
            {
                TempData["ErrorMessage"] = "Ошибка при обновлении студии.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}
