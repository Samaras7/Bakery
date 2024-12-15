namespace Bakery.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;

        public CategoryRepository(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }

        public IEnumerable<Category> AllCategories =>
            _bakeryDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
