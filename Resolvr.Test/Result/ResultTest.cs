using Resolvr;
using Xunit;

namespace Test
{
    public class ResultTest
    {
        private const string ErrorMessage = "error message";
        [Fact]
        public void Result_Default()
        {
            var result = Result.Ok();

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Null(result.ErrorValue);
        }

        [Fact]
        public void Result_Error_Default()
        {
            Result result = Result.Error(ErrorMessage);

            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.NotNull(result.ErrorValue);
            Assert.Equal(ErrorMessage, result.ErrorValue.Message);
        }

        [Fact]
        public void Result_Static_Ok()
        {
            IsOkWithNoValue(Result.Ok());
            IsOkWithValue(Result.Ok(1));
        }
        // [Fact]
        // public void Result_Error_If()
        // {
        //     IsErrorWithMessage(Result.ErrorIf(true, ErrorMessage));
        //     IsErrorWithMessage(Result.ErrorIf(true, ErrorMessage));
        //     IsErrorWithNoMessage(Result.ErrorIf(true));
        //     IsErrorWithNoMessage(Result.ErrorIf(true));
        // }
        [Fact]
        public void Result_Static_Fail()
        {
            IsOkWithNoValue(Result.Ok());
            IsOkWithValue(Result.Ok(1));
        }

        [Fact]
        public void Aceptance()
        {
            Accept<Result>(Result.Ok());
            Accept<Result>(Result.Ok(1));
            Accept<Result>(new Error());
            // Accept<Result>(Result.ErrorIf(true));
            // Accept<Result>(Result.ErrorIf(true, new Error()));

            Accept<Result<int>>(Result.Ok(1));
        }

        private void Accept<T>(T data){ }

        private void IsErrorWithMessage(Result result)
        {
            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.Equal(ErrorMessage, result.ErrorValue.Message);
            Assert.NotNull(result.ErrorValue);
        }
        private void IsErrorWithNoMessage(Result result)
        {
            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.Equal(string.Empty, result.ErrorValue.Message);
            Assert.NotNull(result.ErrorValue);
        }

        private void IsOkWithNoValue(Result result)
        {
            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Null(result.ErrorValue);
        }
        private void IsOkWithValue<T>(Result<T> result)
        {
            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
            Assert.Null(result.ErrorValue);
        }
    }
}

