using AutoMapper;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Event
{
    public class EditModel : PageModel
    {
        private readonly IEventApiService service;
        private readonly IMapper mapper;

        public EditModel(IEventApiService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public EventDTO dto { get; set; }

        [BindProperty]
        public EventDTO New { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var elem = await service.GetEventByIdAsync(id);
            dto = elem;
            New = mapper.Map<EventDTO>(elem); // глубокое копирование
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var existing = await service.GetEventByIdAsync(New.Id);
            if (existing == null)
            {
                TempData["ErrorMessage"] = "Событие не найдено.";
                return RedirectToPage();
            }

            existing.Title = New.Title;
            existing.Address = New.Address;
            existing.Country = New.Country;
            existing.Region = New.Region;
            existing.City = New.City;
            existing.GeoUrl = New.GeoUrl;
            existing.Status = New.Status;
            existing.PriceMin = New.PriceMin;
            existing.PriceMax = New.PriceMax;
            existing.Content = New.Content;
            existing.OrganizerEventId = New.OrganizerEventId;

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

            var success = await service.UpdateEventAsync(existing);
            if (!success)
            {
                TempData["ErrorMessage"] = "Ошибка при сохранении изменений.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}
