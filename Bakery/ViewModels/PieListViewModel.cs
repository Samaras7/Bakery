using Bakery.Models;

namespace Bakery.ViewModels
{
    public class PieListViewModel
    {
        public IEnumerable<Pie> Pies { get; }
        public string? CurrentCategory { get; }
        public string? CategoryFilter { get; }

        public PieListViewModel(IEnumerable<Pie> pies, string? currentCategory, string? categoryFilter)
        {
            Pies = pies;
            CurrentCategory = currentCategory;
            CategoryFilter = categoryFilter;
        }
    }
}
