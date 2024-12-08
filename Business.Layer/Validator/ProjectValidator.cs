using DataTransferObject.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class ProjectValidator:AbstractValidator<RequestProject>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.ProjectName).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.TeamId > 0).NotNull().NotEmpty();
        }
    }
}
