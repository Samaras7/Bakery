using Bakery.Models;
using Bakery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bakery.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Create()
        {
            var viewModel = BuildPieFormViewModel(new Pie { InStock = true });
            ViewData["FormAction"] = nameof(Create);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(Prefix = "Pie")] Pie pie)
        {
            if (!ModelState.IsValid)
            {
                ViewData["FormAction"] = nameof(Create);
                return View(BuildPieFormViewModel(pie));
            }

            var createdPie = _pieRepository.AddPie(pie);
            return RedirectToAction(nameof(List), new { category = createdPie.Category?.CategoryName });
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }

        public IActionResult Edit(int id)
        {
            var pie = _pieRepository.GetPieById(id);

            if (pie == null)
            {
                return NotFound();
            }

            ViewData["FormAction"] = nameof(Edit);
            return View(BuildPieFormViewModel(pie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind(Prefix = "Pie")] Pie pie)
        {
            if (id != pie.PieId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["FormAction"] = nameof(Edit);
                return View(BuildPieFormViewModel(pie));
            }

            var updated = _pieRepository.UpdatePie(pie);

            if (!updated)
            {
                return NotFound();
            }

            var updatedPie = _pieRepository.GetPieById(pie.PieId);
            return RedirectToAction(nameof(List), new { category = updatedPie?.Category?.CategoryName });
        }

        public IActionResult Delete(int id)
        {
            var pie = _pieRepository.GetPieById(id);

            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = _pieRepository.DeletePie(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        private PieFormViewModel BuildPieFormViewModel(Pie pie)
        {
            var categoryOptions = _categoryRepository.AllCategories
                .Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString(),
                    Selected = c.CategoryId == pie.CategoryId
                });

            return new PieFormViewModel
            {
                Pie = pie,
                CategoryOptions = categoryOptions
            };
        }
    }
}
