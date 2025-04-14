using FluentValidation;
using IMS.Domain.Entities;

namespace IMS.Application.Validators
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Category name is required")
				.MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");
		}
	}
}