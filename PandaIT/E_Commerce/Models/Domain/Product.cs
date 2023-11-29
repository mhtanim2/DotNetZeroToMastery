using E_Commerce.Interface;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Models.Domain
{
    public class Product:IUserEntity<Object>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }
        // public string PublicId { get; set; }
    }
}
