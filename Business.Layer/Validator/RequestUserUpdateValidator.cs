using DataTransferObject.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class RequestUserUpdateValidator:AbstractValidator<RequestUserUpdate>
    {
        public RequestUserUpdateValidator()
        {
           
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.SurName).NotNull().NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.TeamId > 0).NotNull().NotEmpty();
        }
    }
}
