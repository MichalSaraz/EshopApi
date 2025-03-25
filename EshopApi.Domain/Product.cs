using System.ComponentModel.DataAnnotations;

namespace EshopApi.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required string PictureUri { get; set; }

        public Product(string name, string description, decimal price, string pictureUri)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
        }
    }
}