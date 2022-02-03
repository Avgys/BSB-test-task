using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }     
        public Category Category { get; set; }
        private string categoryName = "";
        [NotMapped]
        public string CategoryName
        {
            get
            {
                if (Category != null)
                {
                    return Category.Name;
                }
                else
                {
                    return categoryName;
                }
            }
            set
            {
                categoryName = value;
            }
        }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public string Specification { get; set; }
        public string SpecialSpec { get; set; }
    }
}