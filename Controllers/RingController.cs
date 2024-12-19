using Microsoft.AspNetCore.Mvc;
using Power.Domain.RingsAggregate;
using Power.Domain.RingsAggregate.Entities;
using Power.Domain.RingsAggregate.Services;
using Power.ViewModel;

namespace Power.Controllers
{

    public class Return<T>
    {
        public bool Success { get; private set; }
        public List<T>  Data{ get; private set; } = new List<T>();

        public Return(bool success, List<T> data)
        {
            Success = success;
            Data = data;
        }
    }
    public class RingController : Controller
    {
        private readonly IRingService _service;

        public RingController(IRingService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            
            //ViewBag.Title = "Anéis";
            var rings = new List<Ring>()
            {
                new Ring("Teste 1", "", "", ForgedBy.Dwarves, ""),
                new Ring("Teste 2", "", "", ForgedBy.Dwarves, ""),
                new Ring("Teste 3", "", "", ForgedBy.Dwarves, "")
            };

            return PartialView(new Return<Ring>(true, rings));

        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var ring =  await _service.GetById(id);

            if (ring is null)
            {
                return RedirectToAction("Index");
            }

            var model = MapToViewModel(ring);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new RingViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RingViewModel ring)
        {
            if (!ModelState.IsValid)
            {
                return View(ring);
            }

            //await _service.Create(ring.Name, ring.Power, ring.Portador, ring.Forjador, ring.Imagem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var ring =  new Ring("", "", "", ForgedBy.Dwarves, ""); //await _service.GetById(id));

            if (ring is null)
            {
                return RedirectToAction("Index");
            }

            var model = MapToViewModel(ring);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RingViewModel ringModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ringModel);
            }

            /*await _service.Update(
                ringModel.Id,
                ringModel.Name,
                ringModel.Power,
                ringModel.Portador,
                ringModel.Forjador,
                ringModel.Imagem
            );*/

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var ring =  new Ring("", "", "", ForgedBy.Dwarves, ""); //await _service.GetById(id));

            if (ring is null)
            {
                return RedirectToAction("Index");
            }

            var model = MapToViewModel(ring);

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var ring =  new Ring("", "", "", ForgedBy.Dwarves, ""); // await _service.GetById(id);

            if (ring is null)
            {
                return RedirectToAction("Index");
            }

            //await _service.Delete(id);

            return RedirectToAction("Index");
        }

        private RingViewModel MapToViewModel(Ring ring)
        {
            return new RingViewModel
            {
                Id = ring.Id,
                Name = ring.Name,
                Power = ring.Power,
                Carrier = ring.Carrier,
                ForgedBy = ring.ForgedBy,
                Image = ring.Image
            };
        }
    }
}
