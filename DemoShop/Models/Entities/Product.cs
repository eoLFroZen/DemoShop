using System.ComponentModel.DataAnnotations.Schema;

namespace DemoShop.Models.Entities
{
    public class Product
    { 

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
