using FluentValidation;

namespace ApiFinal.App.Dtos.Categories
{
    public record CategoryPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
