namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeQuery : QueryBase<FakeInput, FakeInputValidator>
    {
        protected override string Prefix => "query";
    }
}