using System.Collections;

namespace Foundation.UnitTests.Application.Handlers.Queries
{
    public class QueryHandlerBaseTheories
    {
        public class DoHandleAsyncMethodDataGenerator : IEnumerable<object[]>
        {
            private readonly string[] _emptyStrArr = [];
            private readonly FakeQueryResult? _defaultResult = default;

            public IEnumerator<object[]> GetEnumerator() 
            {
                List<object?[]> data = [
                    ["Test", 12, true, true, _emptyStrArr, _emptyStrArr, new FakeQueryResult { Name = "TEST", Value = 7 }],
                    ["", 12, false, false, _emptyStrArr, new string[] { Constants.NAME_EMPTY }, _defaultResult],
                    ["TestTestTest", 12, false, false, _emptyStrArr, new string[] { Constants.NAME_TOO_LONG }, _defaultResult],
                    ["Test", 8, false, false, _emptyStrArr, new string[] { Constants.BAD_VALUE_RANGE }, _defaultResult],
                    ["Test", 16, true, false, _emptyStrArr, new string[] { Constants.ERROR_VALUE_16 }, _defaultResult]
                ];

                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "QueryHandlerBase DoHandleAsync Tests")]
        [ClassData(typeof(DoHandleAsyncMethodDataGenerator))]
        public async Task DoHandleAsyncMethod(string name, int value, bool isValid, bool isCompleted, string[] warnings, string[] errors, FakeQueryResult? result)
        {
            var query = new FakeQuery { Input = new FakeInput { Name = name, Value = value } };
            var logger = new Mock<ILogger>();
            var handler = new FakeQueryHandler(logger.Object);

            var handlerResult = await handler.HandleAsync(query);

            handlerResult.IsValid.Should().Be(isValid);
            handlerResult.IsCompleted.Should().Be(isCompleted);
            handlerResult.Warnings.Should().BeEquivalentTo(warnings, options => options.WithStrictOrdering());
            handlerResult.Errors.Should().BeEquivalentTo(errors, options => options.WithStrictOrdering());
            handlerResult.Result.Should().BeEquivalentTo(result);
        }
    }
}