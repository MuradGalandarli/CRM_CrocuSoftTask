using FluentValidation;
using Shred.Layer.AuthModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class LoginModelValidator:AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("User is required");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is required");
        }
    
    }
}
