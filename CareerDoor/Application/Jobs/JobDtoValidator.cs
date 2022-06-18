using FluentValidation;

namespace Application.Jobs
{
    class JobDtoValidator : AbstractValidator<JobDto>
    {
        public JobDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Company).NotNull().NotEmpty();
            RuleFor(x => x.Experience).NotNull().NotEmpty();
            RuleFor(x => x.Expiration).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Functionality).NotNull().NotEmpty();
            RuleFor(x => x.Industry).NotNull().NotEmpty();
            RuleFor(x => x.Location).NotNull().NotEmpty();
            RuleFor(x => x.EmployeeCount).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotNull().NotEmpty();
        }
    }
}
