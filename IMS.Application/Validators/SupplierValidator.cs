using FluentValidation;
using IMS.Domain.Entities;

namespace IMS.Application.Validators
{
	public class SupplierValidator : AbstractValidator<Supplier>
	{
		public SupplierValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Supplier name is required")
				.MaximumLength(100).WithMessage("Supplier name cannot exceed 100 characters");

		}
	}
}