using FluentValidation;
using Shred.Layer.AuthModel;

namespace Business.Layer.Validator
{
    public class RegistrationModelValidator:AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.SurName).NotNull().NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.TeamId > 0).NotNull().NotEmpty();
           
        }
    }
}
