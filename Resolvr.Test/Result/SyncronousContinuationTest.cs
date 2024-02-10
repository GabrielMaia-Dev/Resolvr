using Resolvr;
using Resolvr.Continuation;
using Xunit;

namespace Test
{
    public class SyncronousContinuationTest
    {
        [Fact]
        public void SuccessTypedResult_To_Result()
        {
            bool executed = false;
            
            var result = Result.Ok(1)
                .Then((v) =>
                {
                    executed = true;
                    return Result.Ok();
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void ErrorTypedResult_To_Result()
        {
            bool executed = false; 

            var result = Result.Error<int>()
                .Then((v) =>
                {
                    executed = true;
                    return Result.Ok();
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }

        [Fact]
        public void SuccessTypedResult_To_TypedResult()
        {
            bool executed = false;
            var value = 1; 
            
            var result = Result.Ok(1)
                .Then((v) =>
                {
                    executed = true;
                    return Result.Ok(value);
                });

            Assert.IsType<Result<int>>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void ErrorTypedResult_To_TypedResult()
        {
            bool executed = false; 
            var value = 1; 

            var result = Result.Error<int>()
                .Then((v) =>
                {
                    executed = true;
                    return Result.Ok(value);
                });

            Assert.IsType<Result<int>>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }

        [Fact]
        public void SuccessResult_To_TypedResult()
        {
            bool executed = false;
            var value = 1; 
            
            var result = Result.Ok()
                .Then(() =>
                {
                    executed = true;
                    return Result.Ok(value);
                });

            Assert.IsType<Result<int>>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void ErrorResult_To_TypedResult()
        {
            bool executed = false; 
            var value = 1; 

            var result = Result.Error()
                .Then(() =>
                {
                    executed = true;
                    return Result.Ok(value);
                });

            Assert.IsType<Result<int>>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }

        [Fact]
        public void SuccessResult_To_Result()
        {
            bool executed = false; 
            
            var result = Result.Ok()
                .Then(() =>
                {
                    executed = true;
                    return Result.Ok();
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void ErrorResult_To_Result()
        {
            bool executed = false; 
            
            var result = Result.Error()
                .Then(() =>
                {
                    executed = true;
                    return Result.Ok();
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }

        [Fact]
        public void SuceesResult_Action()
        {
            bool executed = false; 
            
            var result = Result.Ok()
                .Then(() => executed = true);

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void ErrorResult_Action()
        {
            bool executed = false; 
            
            var result = Result.Error()
                .Then(() => executed = true);

            Assert.IsType<Result>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }

        [Fact]
        public void TypedSuccessResult_Action()
        {
            bool executed = false;
            var value = 1; 
            
            var result = Result.Ok(1)
                .Then((v) => 
                {
                    Assert.Equal(value, v);
                    executed = true;
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsSuccess);
            Assert.True(executed);
        }

        [Fact]
        public void TypedErrorResult_Action()
        {
            bool executed = false;
            var value = 1; 
            
            var result = Result.Error<int>()
                .Then((v) => 
                {
                    Assert.Equal(value, v);
                    executed = true;
                });

            Assert.IsType<Result>(result);
            Assert.True(result.IsError);
            Assert.False(executed);
        }
    }
}