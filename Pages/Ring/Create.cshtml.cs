using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Power.Domain.RingsAggregate;
using Power.Domain.RingsAggregate.Services;

namespace Power.Pages.Ring
{
    public class CreateModel : PageModel
    {
        private readonly IRingService _service;

        public List<SelectListItem> RingPower { get; private set; } = new List<SelectListItem>();

        public CreateModel(IRingService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            RingPower = GetListOfRingPowers();
            return Page();
        }


        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string Poder { get; set; }

        [BindProperty]
        public string Portador { get; set; }

        [BindProperty]
        public ForgedBy Forjador { get; set; }

        [BindProperty]
        public string Imagem { get; set; }


        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Create(Nome, Poder, Portador, Forjador, Imagem);

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
    }
}
