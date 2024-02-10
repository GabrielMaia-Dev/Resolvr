using Resolvr;
using Xunit;

namespace Test
{
    public class ErrorTest
    {
        private const string ErrorMessage = "error message";

        [Fact]
        public void Error_Implicit_Conversions()
        {
            Error error = ErrorMessage;
            Result result = error;
            Assert.Equal(ErrorMessage, error.Message);
            
            Assert.True(result.IsError);
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage, result.ErrorValue.Message);
        }

        [Fact]
        public void Error_Message_Default_To_EmptyString()
        {
            var error = new Error();
            Assert.Equal(string.Empty, error.Message);
        }
    }
}