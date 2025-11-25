using Bakery.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bakery.ViewModels
{
    public class PieFormViewModel
    {
        public Pie Pie { get; set; } = new();

        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
