using DataComp.Training.IServices;
using DataComp.Training.Models;
using FluentValidation;
using System;

namespace DataComp.Training.Validators
{
    // dotnet add package FluentValidation
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserService userService;

        public UserValidator(IUserService userService)
        {
            this.userService = userService;

            RuleFor(p => p.LastName).NotEmpty().Length(3, 50);
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.Pesel).NotEmpty().Must((u, prop)=>!userService.IsExists(u.Pesel));
        }
    }
}
