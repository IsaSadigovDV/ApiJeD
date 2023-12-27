using ApiFinal.Service.Dtos.Categories;
using ApiFinal.Service.Dtos.Products;
using ApiFinal.Service.Extentions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFinal.Service.Validations.Products
{
    public class ProductPostDtoValidation : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x).Custom((x, context) =>
            {
                if (!x.File.IsImage())
                {
                    context.AddFailure("File", "The file is not valid image");
                }
                if (!x.File.IsSizeOk(2))
                {
                    context.AddFailure("File", "The file size max length must be 2 mb");
                }
            });
        }
    }
}
