using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Application.Messages.Commands
{
    public class CommandBaseTheories
    {
        [Theory(DisplayName = "CommandBase ValidateAsync Tests")]
        [InlineData("Test", 12, true, new string[0], new string[0])]
        [InlineData("", 12, false, new string[0], new string[] { Constants.NAME_EMPTY })]
        [InlineData("TestTestTest", 12, false, new string[0], new string[] { Constants.NAME_TOO_LONG })]
        [InlineData("Test", 8, false, new string[0], new string[] { Constants.BAD_VALUE_RANGE })]
        public async Task ValidateAsyncMethod(string name, int value, bool isValid, string[] warnings, string[] errors)
        {
            var msg = new FakeCommand { Input = new FakeInput { Name = name, Value = value } };

            var validation = await msg.ValidateAsync();

            validation.IsValid.Should().Be(isValid);
            validation.Warnings.Should().BeEquivalentTo(warnings, options => options.WithStrictOrdering());
            validation.Errors.Should().BeEquivalentTo(errors, options => options.WithStrictOrdering());
        }
    }
}