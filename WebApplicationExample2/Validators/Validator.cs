using FluentValidation;
using WebApplicationExample2.Models;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(person => person.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(person => person.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age must be a non-negative value.")
            .LessThanOrEqualTo(150).WithMessage("Age cannot exceed 150.");
    }
}
