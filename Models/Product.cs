using System.ComponentModel.DataAnnotations;

namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }     
        public Category category { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public string Specification { get; set; }
        public string SpecialSpec { get; set; }
    }
}