namespace EshopApi.Shared.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public required decimal Price { get; set; }
        public required string PictureUri { get; set; }
    }
}