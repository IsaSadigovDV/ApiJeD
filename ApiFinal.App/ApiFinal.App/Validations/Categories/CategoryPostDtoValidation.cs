﻿using ApiFinal.App.Dtos.Categories;
using FluentValidation;

namespace ApiFinal.App.Validations.Categories
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

            RuleFor(x => x.Description).NotEmpty()
                                .WithMessage("Description can not empty")
                                .NotNull()
                                .WithMessage("Description can not null")
                                .MinimumLength(3)
                                .MaximumLength(100);
        }
    }
}
