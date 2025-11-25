using System;
using System.Collections.Generic;
using System.Linq;

namespace Bakery.Models
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();
        private readonly List<Pie> _pies;

        public MockPieRepository()
        {
            var categories = _categoryRepository.AllCategories.ToList();

            _pies = new List<Pie>
            {
                new Pie
                {
                    PieId = 1,
                    Name = "Strawberry Pie",
                    Price = 15.95M,
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    Category = categories[0],
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/strawberrypie.jpg",
                    InStock = true,
                    IsPieOfTheWeek = false,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/strawberrypiesmall.jpg"
                },
                new Pie
                {
                    PieId = 2,
                    Name = "Cheese cake",
                    Price = 18.95M,
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    Category = categories[1],
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecake.jpg",
                    InStock = true,
                    IsPieOfTheWeek = false,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/cheesecakes/cheesecakesmall.jpg"
                },
                new Pie
                {
                    PieId = 3,
                    Name = "Rhubarb Pie",
                    Price = 15.95M,
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    Category = categories[0],
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/rhubarbpie.jpg",
                    InStock = true,
                    IsPieOfTheWeek = true,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/fruitpies/rhubarbpiesmall.jpg"
                },
                new Pie
                {
                    PieId = 4,
                    Name = "Pumpkin Pie",
                    Price = 12.95M,
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    Category = categories[2],
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/pumpkinpie.jpg",
                    InStock = true,
                    IsPieOfTheWeek = true,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/bethanyspieshop/seasonal/pumpkinpiesmall.jpg"
                }
            };
        }

        public IEnumerable<Pie> AllPies => _pies;

        public IEnumerable<Pie> PiesOfTheWeek => _pies.Where(p => p.IsPieOfTheWeek);

        public Pie? GetPieById(int pieId) => _pies.FirstOrDefault(p => p.PieId == pieId);

        public Pie AddPie(Pie pie)
        {
            var nextId = _pies.Count == 0 ? 1 : _pies.Max(p => p.PieId) + 1;
            pie.PieId = nextId;
            pie.Category ??= _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryId == pie.CategoryId);
            pie.CategoryId = pie.Category?.CategoryId ?? pie.CategoryId;
            _pies.Add(pie);
            return pie;
        }

        public bool UpdatePie(Pie pie)
        {
            var existing = _pies.FirstOrDefault(p => p.PieId == pie.PieId);
            if (existing == null)
            {
                return false;
            }

            existing.Name = pie.Name;
            existing.Price = pie.Price;
            existing.ShortDescription = pie.ShortDescription;
            existing.LongDescription = pie.LongDescription;
            existing.ImageUrl = pie.ImageUrl;
            existing.ImageThumbnailUrl = pie.ImageThumbnailUrl;
            existing.IsPieOfTheWeek = pie.IsPieOfTheWeek;
            existing.InStock = pie.InStock;
            existing.Category = pie.Category ?? _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryId == pie.CategoryId);
            existing.CategoryId = existing.Category?.CategoryId ?? pie.CategoryId;

            return true;
        }

        public bool DeletePie(int pieId)
        {
            var existing = _pies.FirstOrDefault(p => p.PieId == pieId);
            if (existing == null)
            {
                return false;
            }

            _pies.Remove(existing);
            return true;
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return _pies;
            }

            return _pies.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
        }
    }
}
