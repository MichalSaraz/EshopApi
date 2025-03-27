using System.ComponentModel.DataAnnotations;

namespace EshopApi.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required string PictureUri { get; set; }
    }
}