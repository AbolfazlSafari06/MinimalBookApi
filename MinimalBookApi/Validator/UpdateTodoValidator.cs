using FluentValidation;
using MinimalBookApi.Models;

namespace MinimalBookApi.Validator;

public class UpdateTodoValidator : AbstractValidator<UpdateTodoDto>
{
    const string RequiredMessage = "این فیلد الزامی است";
    public string MinLength(int count) => $"این فیلد نباید کمتر از {count} حرف باشد";
    public string MaxLengthError(int count) => $"این فیلد نباید بیشتر از {count} حرف باشد";

    public UpdateTodoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .MinimumLength(3)
            .WithMessage(MinLength(3))
            .MaximumLength(250)
            .WithMessage(MaxLengthError(250));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(RequiredMessage)
            .MinimumLength(3)
            .WithMessage(MinLength(3))
            .MaximumLength(250)
            .WithMessage(MaxLengthError(250));

    }
}