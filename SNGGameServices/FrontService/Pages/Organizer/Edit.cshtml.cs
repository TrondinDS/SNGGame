using AutoMapper;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Security;

namespace FrontService.Pages.Organizer
{
    public class EditModel : PageModel
    {
        private readonly IOrganizerService service;
        private readonly IMapper mapper;

        public EditModel(IOrganizerService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public OrganizerDTO dto { get; set; }
        [BindProperty]
        public OrganizerDTO New { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var elem = await service.GetById(id);
            if (elem == null) return NotFound();
            dto = elem;
            New = mapper.Map<OrganizerDTO>(elem); // глубокое копирование
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var existing = await service.GetById(New.Id);
            if (existing == null)
            {
                TempData["ErrorMessage"] = "Организатор не найден.";
                return RedirectToPage();
            }

            existing.Title = New.Title;
            existing.Mail = New.Mail;
            existing.Content = New.Content;

            if (ImageFile is { Length: > 0 })
            {
                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(ImageFile.ContentType))
                {
                    TempData["ErrorMessage"] = "Разрешены только изображения JPG и PNG.";
                    return RedirectToPage();
                }

                try
                {
                    using var memoryStream = new MemoryStream();
                    await ImageFile.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    existing.Image = Convert.ToBase64String(fileBytes);
                    existing.ImageType = ImageFile.ContentType;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ошибка обработки изображения: {ex.Message}";
                    return RedirectToPage();
                }
            }

            var success = await service.Update(existing);
            if (!success)
            {
                TempData["ErrorMessage"] = "Ошибка при сохранении изменений.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}
