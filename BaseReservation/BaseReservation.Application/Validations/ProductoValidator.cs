using BaseReservation.Infrastructure.Models;
using FluentValidation;

namespace BaseReservation.Application.Validations;

public class ProductoValidator : AbstractValidator<Producto>
{
    public ProductoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("Por favor ingrese el nombre")
            .MaximumLength(80).WithMessage("El nombre no puede tener más de 80 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("Por favor ingrese la descripción")
            .MaximumLength(150).WithMessage("La descripción no puede tener más de 150 caracteres");

        RuleFor(x => x.Marca)
            .NotEmpty().WithMessage("Por favor ingrese la marca")
            .MaximumLength(50).WithMessage("La marca no puede tener más de 50 caracteres");

        RuleFor(x => x.IdCategoria)
            .NotEmpty().WithMessage("Por favor ingrese una categoría válida");

        RuleFor(x => x.Costo)
            .GreaterThan(0).WithMessage("El costo debe ser mayor a 0");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Por favor ingrese el SKU")
            .MaximumLength(30).WithMessage("El SKU no puede tener más de 30 caracteres");

        RuleFor(x => x.IdUnidadMedida)
            .NotEmpty().WithMessage("Por favor ingrese una unidad de medida válida");
    }
}