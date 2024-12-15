using Bakery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = pieRepository.AllPies;
            return Ok(allPies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var specificPie = pieRepository.AllPies.Any(i => i.PieId == id);
            if (!specificPie)
                return NotFound();

            return Ok(specificPie);
        }
    }
}
