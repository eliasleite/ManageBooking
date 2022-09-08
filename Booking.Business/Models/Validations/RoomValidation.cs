using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Models.Validations
{
    public class RoomValidation: AbstractValidator<Room>
    {
        public RoomValidation()
        {
            RuleFor(r => r.Number)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided");

            RuleFor(r => r.Floor)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided");

            RuleFor(r => r.Price)
                .GreaterThan(0).WithMessage("The field {PropertyName} needs to be greater then {ComparisonValue}");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided")
                .Length(2, 1000).WithMessage("The field {PropertyName} needs to be between {MinLength} and {MaxLength} characters");
        }
    }
}
