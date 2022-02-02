
namespace Catalog.Models
{
    public class ProductDTO {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string CategoryName { get; set; }
        public string Description { get; set; }        
        public int Price { get; set; }
        public string Specification { get; set; }
        public string SpecialSpec { get; set; }
    }
}