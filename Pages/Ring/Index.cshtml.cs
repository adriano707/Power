using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Power.Data;
using Power.Domain.RingsAggregate.Entities;
using Power.Domain.RingsAggregate.Services;

namespace Power.Pages.Ring
{
    public class IndexModel : PageModel
    {
        private readonly IRingService _service;

        public IndexModel(IRingService service)
        {
            _service = service;
        }

        public IList<Domain.RingsAggregate.Entities.Ring> Ring { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                Ring = await _service.GetAll();
            }
            catch (Exception ex)
            {
                Ring = new List<Domain.RingsAggregate.Entities.Ring>();
            }
        }
    }
}
