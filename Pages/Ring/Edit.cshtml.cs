using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Power.Domain.RingsAggregate.Services;

namespace Power.Pages.Ring
{
    public class EditModel : PageModel
    {
        private readonly IRingService _service;

        public EditModel(IRingService service)
        {
            _service = service;
        }

        [BindProperty]
        public Domain.RingsAggregate.Entities.Ring Ring { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Ring = await _service.GetById(id.Value);

            if (Ring == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Update(Ring.Id, Ring.Name, Ring.Power, Ring.Carrier, Ring.ForgedBy, Ring.Image);

            return RedirectToPage("./Index");
        }

        private bool RingExists(Guid id)
        {
            var ring = _service.GetById(id);
            return ring != null;
        }
    }
}