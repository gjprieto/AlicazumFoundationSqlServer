namespace Foundation.UnitTests.Application.Handlers
{
    public class MessageHandlerBaseTheories
    {   
        [Theory(DisplayName = "MessageHandlerBase DoHandleAsync Tests")]
        [InlineData("Test", 12, true, true, new string[0], new string[0])]
        [InlineData("", 12, false, false, new string[0], new string[] { Constants.NAME_EMPTY })]
        [InlineData("TestTestTest", 12, false, false, new string[0], new string[] { Constants.NAME_TOO_LONG })]
        [InlineData("Test", 8, false, false, new string[0], new string[] { Constants.BAD_VALUE_RANGE })]
        [InlineData("Test", 16, true, false, new string[0], new string[] { Constants.ERROR_VALUE_16 })]
        public async Task DoHandleAsyncMethod(string name, int value, bool isValid, bool isCompleted, string[] warnings, string[] errors)
        {
            var msg = new FakeMessage { Input = new FakeInput { Name = name, Value = value } };
            var logger = new Mock<ILogger>();
            var handler = new FakeMessageNoResultHandler(logger.Object);

            var handlerResult = await handler.HandleAsync(msg);

            handlerResult.IsValid.Should().Be(isValid);
            handlerResult.IsCompleted.Should().Be(isCompleted);
            handlerResult.Warnings.Should().BeEquivalentTo(warnings, options => options.WithStrictOrdering());
            handlerResult.Errors.Should().BeEquivalentTo(errors, options => options.WithStrictOrdering());
        }
    }
}