using FluentValidation;

namespace TaskInsightEngine.Application.Dtos.Risk.Validators
{
    public class CreateRiskSubscriptionRequestValidator : AbstractValidator<CreateRiskSubscriptionRequest>
    {
        public CreateRiskSubscriptionRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Hours).InclusiveBetween(0, 23);
            RuleFor(x => x.Minutes).InclusiveBetween(0, 59);
        }
    }
}
