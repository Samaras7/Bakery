using Bakery.Models;
using Bakery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Create(string? category)
        {
            var pie = new Pie { InStock = true };

            if (!string.IsNullOrWhiteSpace(category))
            {
                var matchingCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category);
                if (matchingCategory != null)
                {
                    pie.CategoryId = matchingCategory.CategoryId;
                }
            }

            var viewModel = BuildPieFormViewModel(pie, category);
            ViewData["FormAction"] = nameof(Create);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(Prefix = "Pie")] Pie pie, string? returnCategory)
        {
            ClearNonRequiredValidationErrors();

            if (!ModelState.IsValid)
            {
                ViewData["FormAction"] = nameof(Create);
                return View(BuildPieFormViewModel(pie, returnCategory));
            }

            var createdPie = _pieRepository.AddPie(pie);
            var destinationCategory = returnCategory ?? createdPie.Category?.CategoryName;
            return RedirectToAction(nameof(List), new { category = destinationCategory });
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

            ClearNonRequiredValidationErrors();

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

            return View(new PieListViewModel(pies, currentCategory, category));
        }

        private PieFormViewModel BuildPieFormViewModel(Pie pie, string? returnCategory = null)
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
                CategoryOptions = categoryOptions,
                ReturnCategory = returnCategory
            };
        }

        private void ClearNonRequiredValidationErrors()
        {
            var requiredKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Pie.Name",
                "Pie.Price",
                "Pie.CategoryId"
            };

            var keysToClear = ModelState.Keys.Where(k => !requiredKeys.Contains(k)).ToList();

            foreach (var key in keysToClear)
            {
                ModelState[key].Errors.Clear();
            }
        }
    }
}
