using System.ComponentModel.DataAnnotations;

namespace ECartBulkyWebBooks.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }

        public string DisplayOrder { get; set; }
    }
}
