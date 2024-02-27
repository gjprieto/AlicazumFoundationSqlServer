namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeCommand : CommandBase<FakeInput, FakeInputValidator>
    {
        protected override string Prefix => "comm";
    }
}