namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeInputValidator : AbstractValidator<FakeInput>
    {
        public FakeInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Constants.NAME_EMPTY).MaximumLength(10).WithMessage(Constants.NAME_TOO_LONG);
            RuleFor(x => x.Value).InclusiveBetween(10, 20).WithMessage(Constants.BAD_VALUE_RANGE);
        }
    }
}
