namespace EshopApi.WebApi.Api.Models
{
    /// <summary>
    /// Model for updating the description of a product.
    /// </summary>
    public class UpdateDescriptionModel
    {
        /// <summary>
        /// Gets or sets the new description for the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}