using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.User;
using FluentValidation;

namespace Ecommerce.Validations.User
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidation()
        {
            RuleFor(u => u.username).NotEmpty().NotNull();
            RuleFor(u => u.password).NotEmpty().NotNull();
        }
    }
}