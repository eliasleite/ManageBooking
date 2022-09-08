using Booking.Business.Interfaces;
using Booking.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Booking.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        public BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notificate(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificate(error.ErrorMessage);
            }
        }

        protected void Notificate(string message)
        {
            _notificator.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notificate(validator);

            return false;
        }
    }
}
