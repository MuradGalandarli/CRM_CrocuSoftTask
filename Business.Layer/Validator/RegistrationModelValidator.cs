using FluentValidation;
using Shred.Layer.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class RegistrationModelValidator:AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
           
        }
    }
}
