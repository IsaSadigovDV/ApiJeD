namespace ApiFinal.Service.Dtos.Categories
{
    public record CategoryPostDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
