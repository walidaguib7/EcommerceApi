using Ecommerce.Dtos.Profiles;
using Ecommerce.Helpers;
using FluentValidation;

namespace Ecommerce.Validations.Profile
{
    public class ProfileValidation : AbstractValidator<CreateProfileDto>
    {
        public ProfileValidation() {

            RuleFor(p => p.first_name)
                .NotNull().NotEmpty()
                .WithMessage("First name shouldn't be null or empty");
            RuleFor(p => p.last_name)
                .NotNull().NotEmpty()
                .WithMessage("Last name shouldn't be null or empty");
            RuleFor(p => p.age)
                .GreaterThan(15).NotNull()
                .WithMessage("user age should be above of 15");
            RuleFor(p => p.country)
                .NotNull().NotEmpty();
            RuleFor(p => p.city)
                .NotNull().NotEmpty();
            RuleFor(p => p.ZipCode)
                .NotNull().GreaterThan(0);
            RuleFor(p => p.fileId)
                .NotNull().GreaterThan(0);
            RuleFor(p => p.userId)
                .NotEmpty().NotNull();
         

                
        
        }
    }
}
