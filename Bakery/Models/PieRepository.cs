using Microsoft.EntityFrameworkCore;

namespace Bakery.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;

        public PieRepository(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _bakeryDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bakeryDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _bakeryDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
