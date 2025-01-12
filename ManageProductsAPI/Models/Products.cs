using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageProductsAPI.Models
{
    public class Product
    {
        [Key]  // Marks this property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Configures the column to be an identity column
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
        public string Description { get; set; }

    }
}
