using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Power.Pages.Ring
{
    public class DetailsModel : PageModel
    {
        private readonly Power.Data.PowerDbContext _context;

        public DetailsModel(Power.Data.PowerDbContext context)
        {
            _context = context;
        }

        public Domain.RingsAggregate.Entities.Ring Ring { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var ring = await _context.Ring.FirstOrDefaultAsync(m => m.Id == id);
            if (ring is null)
            {
                return NotFound();
            }
            else
            {
                Ring = ring;
            }
            return Page();
        }
    }
}
