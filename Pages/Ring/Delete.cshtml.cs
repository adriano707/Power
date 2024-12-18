using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Power.Data;
using Power.Domain.RingsAggregate.Entities;

namespace Power.Pages.Ring
{
    public class DeleteModel : PageModel
    {
        private readonly Power.Data.PowerDbContext _context;

        public DeleteModel(Power.Data.PowerDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ring = await _context.Ring.FindAsync(id);
            if (ring != null)
            {
                Ring = ring;
                _context.Ring.Remove(Ring);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
