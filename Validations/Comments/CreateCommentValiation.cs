using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dtos.Comments;
using FluentValidation;

namespace Ecommerce.Validations.Comments
{
    public class CreateCommentValiation : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValiation()
        {
            RuleFor(c => c.Content)
            .NotEmpty().NotNull().MaximumLength(255);
            RuleFor(c => c.UserId)
            .NotNull().NotEmpty();
        }
    }
}