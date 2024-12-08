using DataTransferObject.RequestDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class ReportValidator:AbstractValidator<RequestReport>
    {
        public ReportValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.ProjectId > 0).NotEmpty().NotNull();  
        }
    }
}
