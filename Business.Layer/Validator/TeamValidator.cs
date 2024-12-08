using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Validator
{
    public class TeamValidator:AbstractValidator<RequestTeam>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.TeamMembers).NotEmpty().NotNull();
            
        }
    }
}
