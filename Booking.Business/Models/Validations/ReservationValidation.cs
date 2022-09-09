using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Business.Models.Validations
{
    public class ReservationValidation : AbstractValidator<Reservation>
    {
        public ReservationValidation()
        {
            RuleFor(r => r.ReservationDate)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided")
                .Must(BeAValidDate).WithMessage("The field {PropertyName} needs to be valid")
                .LessThanOrEqualTo(r => r.StartDate.AddDays(30)).WithMessage("It's not possible to be reserved more than 30 days in advance");

            RuleFor(r => r.StartDate)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided")
                .Must(BeAValidDate).WithMessage("The field {PropertyName} needs to be valid")
                .GreaterThanOrEqualTo(r => r.ReservationDate.AddDays(1)).WithMessage("The start date for the reservation should be at least 1 day after the current date.");

            RuleFor(r => r.EndDate)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided")
                .Must(BeAValidDate).WithMessage("The field {PropertyName} needs to be valid")
                .GreaterThan(r => r.StartDate).WithMessage("The end date should be greater than the start date.")
                .LessThanOrEqualTo(r => r.StartDate.AddDays(3)).WithMessage("The stay cannot be longer than 3 days from the start date.");

            RuleFor(r => r.Price)
                .NotEmpty().WithMessage("The field {PropertyName} needs to be provided")
                .GreaterThan(0).WithMessage("The field {PropertyName} needs to be valid");

        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }


    }
}
