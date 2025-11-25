using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bakery.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        public string? AllergyInformation { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }

        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }

        [DisplayName("Thumbnail URL")]
        public string? ImageThumbnailUrl { get; set; }

        [DisplayName("Pie of the Week")]
        public bool IsPieOfTheWeek { get; set; }

        [DisplayName("In Stock")]
        public bool InStock { get; set; }

        [DisplayName("Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;
    }
}
