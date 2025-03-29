using System.ComponentModel.DataAnnotations;

namespace EshopApi.Domain.Entities
{
    /// <summary>
    /// Represents a product in the e-commerce system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Price of the product.
        /// </summary>
        [Required]
        public required decimal Price { get; set; }

        /// <summary>
        /// URI of the product picture.
        /// </summary>
        [Required]
        public required string PictureUri { get; set; }
    }
}