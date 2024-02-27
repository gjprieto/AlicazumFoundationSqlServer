namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeMessageNoResultHandler(ILogger logger) : MessageHandlerBase<FakeMessage>(logger)
    {
        protected override async Task<IMessageValidationResult> ValidateHandlerAsync(FakeMessage message)
        {
            await Task.CompletedTask;

            if (message.Input.Value == 15) return MessageValidationResult.GetErrorResult([Constants.ERROR_VALUE_15]);
            return MessageValidationResult.GetOkResult();
        }

        protected override async Task<IMessageHandlerResult> DoHandleAsync(FakeMessage message)
        {
            await Task.CompletedTask;

            if (message.Input.Value == 16) throw new InvalidOperationException(Constants.ERROR_VALUE_16);
            return MessageHandlerResult.GetOkResult();
        }
    }
}