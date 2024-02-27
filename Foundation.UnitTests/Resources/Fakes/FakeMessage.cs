namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeMessage : MessageBase<FakeInput, FakeInputValidator>
    {
        protected override string Prefix => "test";
    }
}