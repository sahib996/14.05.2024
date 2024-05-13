using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(24), Required]
        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SellPrice { get; set; }
        [Range(0, 100)]
        public int Discount { get; set; }

        public int StockCount { get; set; }
        [Required]
        public string IamgeUrl { get; set; }

        public float Raiting { get; set; }

        public ICollection<ProductImage>? Images { get; set; }

        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
