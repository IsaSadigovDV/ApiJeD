namespace ApiFinal.App.Dtos.Categories
{
    public record CategoryUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
