using FluentValidation;

namespace wipro.teste.api.Controllers.PostItem
{
    public class ItemFilaValidator: AbstractValidator<IList<PostItemRequest>>
    {
        public ItemFilaValidator()
        {
            RuleForEach(items => items).SetValidator(valodator => new ItemFilaValidatorInnerItem());
        }
    }

    public class ItemFilaValidatorInnerItem : AbstractValidator<PostItemRequest>
    {
        public ItemFilaValidatorInnerItem()
        {
            RuleFor(item => item.Moeda).NotNull().NotEmpty().WithMessage("É necessário informar a moeda");

            RuleFor(item => item.DataInicio).GreaterThan(data => DateTime.MinValue).WithMessage("É necessário informar uma data de início válida");

            RuleFor(item => item.DataFim).GreaterThan(data => DateTime.MinValue).WithMessage("É necessário informar uma data final válida");
        }
    }
}
