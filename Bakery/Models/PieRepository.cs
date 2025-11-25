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
            return _bakeryDbContext.Pies.Include(p => p.Category).FirstOrDefault(p => p.PieId == pieId);
        }

        public Pie AddPie(Pie pie)
        {
            pie.Category = _bakeryDbContext.Categories.FirstOrDefault(c => c.CategoryId == pie.CategoryId);
            _bakeryDbContext.Pies.Add(pie);
            _bakeryDbContext.SaveChanges();
            _bakeryDbContext.Entry(pie).Reference(p => p.Category).Load();
            return pie;
        }

        public bool UpdatePie(Pie pie)
        {
            var exists = _bakeryDbContext.Pies.AsNoTracking().Any(p => p.PieId == pie.PieId);

            if (!exists)
            {
                return false;
            }

            pie.Category = _bakeryDbContext.Categories.FirstOrDefault(c => c.CategoryId == pie.CategoryId);
            _bakeryDbContext.Pies.Update(pie);
            _bakeryDbContext.SaveChanges();
            return true;
        }

        public bool DeletePie(int pieId)
        {
            var pie = _bakeryDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);

            if (pie == null)
            {
                return false;
            }

            _bakeryDbContext.Pies.Remove(pie);
            _bakeryDbContext.SaveChanges();
            return true;
        }
    }
}
