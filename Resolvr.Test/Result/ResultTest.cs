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
            var result = new Result();

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Null(result.ObjectValue);
            Assert.Null(result.Error);
        }
        [Fact]
        public void Result_Object_Default()
        {
            var obj = new object {};
            var result = new Result(obj);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.NotNull(result.ObjectValue);
            Assert.Same(obj, result.ObjectValue);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Result_Error_Default()
        {
            Result result = new Result(new Error(ErrorMessage));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.Null(result.ObjectValue);
            Assert.NotNull(result.Error);
            Assert.Equal(ErrorMessage, result.Error.Message);
        }

        [Fact]
        public void Result_Static_Ok()
        {
            IsOkWithNoValue(Result.Ok());
            IsOkWithNoValue(Result.Ok<int>());
            IsOkWithValue(Result.Ok(1));
        }
        [Fact]
        public void Result_Error_If()
        {
            IsErrorWithMessage(Result.ErrorIf(true, ErrorMessage));
            IsErrorWithMessage(Result.ErrorIf(true, ErrorMessage));
            IsErrorWithNoMessage(Result.ErrorIf(true));
            IsErrorWithNoMessage(Result.ErrorIf(true));
        }
        [Fact]
        public void Result_Static_Fail()
        {
            IsOkWithNoValue(Result.Ok());
            IsOkWithNoValue(Result.Ok<int>());
            IsOkWithValue(Result.Ok(1));
        }

        [Fact]
        public void Aceptance()
        {
            Accept<Result>(Result.Ok());
            Accept<Result>(Result.Ok<int>());
            Accept<Result>(Result.Ok(1));
            Accept<Result>(new Error());
            Accept<Result>(Result.ErrorIf(true));
            Accept<Result>(Result.ErrorIf<int>(true));
            Accept<Result>(Result.ErrorIf(true, new Error()));
            Accept<Result>(Result.ErrorIf<int>(true, new Error()));

            Accept<Result<int>>(Result.Ok(1));
            Accept<Result<int>>(Result.Ok<int>());
            Accept<Result<int>>(Result.ErrorIf<int>(true));
            Accept<Result<int>>(Result.ErrorIf<int>(true, new Error()));
        }

        private void Accept<T>(T data){ }

        private void IsErrorWithMessage(Result result)
        {
            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.Equal(ErrorMessage, result.Error.Message);
            Assert.Null(result.ObjectValue);
            Assert.NotNull(result.Error);
        }
        private void IsErrorWithNoMessage(Result result)
        {
            Assert.False(result.IsSuccess);
            Assert.True(result.IsError);
            Assert.Equal(string.Empty, result.Error.Message);
            Assert.Null(result.ObjectValue);
            Assert.NotNull(result.Error);
        }

        private void IsOkWithNoValue(Result result)
        {
            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Null(result.ObjectValue);
            Assert.Null(result.Error);
        }
        private void IsOkWithValue(Result result)
        {
            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.NotNull(result.ObjectValue);
            Assert.Null(result.Error);
        }
    }
}

