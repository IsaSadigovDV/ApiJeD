using ApiFinal.Service.Dtos.Categories;
using FluentValidation;

namespace ApiFinal.Service.Validations.Categories
{
    public class CategoryPostDtoValidation : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Name can not empty")
                                .NotNull()
                                .WithMessage("Name can not null")
                                .MinimumLength(3)
                                .MaximumLength(30);
        }
    }
}
