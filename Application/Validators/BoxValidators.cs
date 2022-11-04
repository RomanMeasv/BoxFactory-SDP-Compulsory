using Application.DTOs;
using Domain;
using FluentValidation;

namespace Application.Validators;

public class PostBoxValidator : AbstractValidator<PostBoxDTO>
{
    public PostBoxValidator()
    {
        RuleFor(box => box.Width).GreaterThan(0);
        RuleFor(box => box.Height).GreaterThan(0);
    }
}

public class BoxValidator : AbstractValidator<Box>
{
    public BoxValidator()
    {
        RuleFor(box => box.Width).GreaterThan(0);
        RuleFor(box => box.Height).GreaterThan(0);
    }
}