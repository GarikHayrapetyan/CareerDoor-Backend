﻿using Domain;
using FluentValidation;


namespace Application.Jobs
{
    public class JobValidator : AbstractValidator<Job>
    {
        public JobValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Company).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Function).NotNull().NotEmpty();
            RuleFor(x => x.Industry).NotNull().NotEmpty();
            RuleFor(x => x.Location).NotNull().NotEmpty();
            RuleFor(x => x.EmployeeCount).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotNull().NotEmpty();
        }
    }
}