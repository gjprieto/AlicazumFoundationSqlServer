namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeQueryHandler(ILogger logger) : QueryHandlerBase<FakeQuery, FakeQueryResult>(logger)
    {
        protected override async Task<IMessageValidationResult> ValidateHandlerAsync(FakeQuery message)
        {
            await Task.CompletedTask;

            if (message.Input.Value == 15) return MessageValidationResult.GetErrorResult([Constants.ERROR_VALUE_15]);
            return MessageValidationResult.GetOkResult();
        }

        protected override async Task<IMessageHandlerResult<FakeQueryResult>> DoHandleAsync(FakeQuery message)
        {
            await Task.CompletedTask;

            if (message.Input.Value == 16) throw new InvalidOperationException(Constants.ERROR_VALUE_16);
            return MessageHandlerResult<FakeQueryResult>.GetOkResult(new FakeQueryResult { Name = message.Input.Name.ToUpper(), Value = message.Input.Value - 5 });
        }
    }
}