using DataTransferObject.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class RequestUserValidator:AbstractValidator<RequestUser>
    {

        public RequestUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.SurName).NotNull().NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.TeamId > 0).NotNull().NotEmpty();
        }
    }
}
