using System.ComponentModel.DataAnnotations;

namespace ECartBulkyWebBooks.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        [Required]
        public required string Description { get; set; }

        public required string DisplayOrder { get; set; }
    }
}
