using Domain;
using FluentValidation;

namespace Application.GetTogethers
{
    public class GetTogetherValidator:AbstractValidator<GetTogether>
    {
        public GetTogetherValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.Link).NotNull().NotEmpty();
            RuleFor(x => x.PassCode).NotNull().NotEmpty();
        }
    }
}
