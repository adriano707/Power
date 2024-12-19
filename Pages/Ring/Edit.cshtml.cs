using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Power.Domain.RingsAggregate;
using Power.Domain.RingsAggregate.Services;

namespace Power.Pages.Ring
{
    public class EditModel : PageModel
    {
        private readonly IRingService _service;

        public List<SelectListItem> RingPower { get; private set; } = new List<SelectListItem>();

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

            ViewData["ForgedByList"] = new SelectList(Enum.GetValues(typeof(ForgedBy)));

            return Page();
        }

        public IActionResult GetListSelect()
        {
            RingPower = GetListOfRingPowers();
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

        private List<SelectListItem> GetListOfRingPowers()
        {
            return Enum.GetValues(typeof(ForgedBy))
                .Cast<ForgedBy>()
                .Select(x => new SelectListItem()
                {
                    Value = ((int)x).ToString(),
                    Text = x.ToString()
                })
                .ToList();
        }

        private bool RingExists(Guid id)
        {
            var ring = _service.GetById(id);
            return ring != null;
        }
    }
}