using System;
using System.Threading.Tasks;
using Resolvr;
using Resolvr.Continuation;
using Xunit;

namespace Test
{
    public class AsynchronousContinuationTest
    {
        private bool executed = false;
        private Task<Result> TaskResult() {
            executed = true;
            return Task.FromResult(Result.Ok());
        }
        private Task<Result<int>> TaskResultInt() {
            executed = true;
            return Task.FromResult(Result.Ok(1));
        }
        [Fact]
        public async void NonTyped_To_NonTyped()
        {
            var result = await Result.Ok()
                .ThenAsync(TaskResult);
            
            Assert.IsType<Result>(result);
            Assert.True(executed);
        }
        [Fact]
        public async void NonTyped_To_Typed()
        {
            var result = await Result.Ok()
                .ThenAsync(TaskResultInt);
            
            Assert.IsType<Result<int>>(result);
            Assert.True(executed);
        }
        [Fact]
        public async void NonTyped_To_Typed_Infered()
        {
            var result = await Result.Ok()
                .ThenAsync(async () => {
                    executed = true;
                    return await Task.FromResult(1);
                });
                
            Assert.IsType<Result<int>>(result);
            Assert.True(executed);
        }
        [Fact]
        public async void Typed_To_NonTyped()
        {
            var result = await Result.Ok(1)
                .ThenAsync(async (value) => await Task.FromResult(Result.Ok()));
        }
        [Fact]
        public async void Typed_To_Typed()
        {
            var result = await Result.Ok(1)
                .ThenAsync(async (value) => await Task.FromResult(Result.Ok(2)));
        }
        [Fact]
        public async void Typed_To_Typed_Infered()
        {
            var result = await Result.Ok(1)
                .ThenAsync(async (value) => await Task.FromResult(2));
        }
        [Fact]
        public async void NonTyped_To_NonTyped_Sync_Continuation()
        {
            var result = await Task.FromResult(Result.Ok())
                .ThenAsync(() => Result.Ok())
                ;
        }
        [Fact]
        public async void NonTyped_To_NonTyped_Async_Continuation()
        {
            var result = await Task.FromResult(Result.Ok())
                .ThenAsync(async () => await Task.FromResult(Result.Ok()))
                ;
        }
        [Fact]
        public async void NonTyped_To_Typed_Sync_Continuation()
        {
            var result = await Task.FromResult(Result.Ok())
                .ThenAsync(() => Result.Ok(1))
                ;
        }
        [Fact]
        public async void NonTyped_To_Typed_Async_Continuation()
        {
            var result = await Task.FromResult(Result.Ok())
                .ThenAsync(async () => await Task.FromResult(Result.Ok(1)))
                ;
        }
        [Fact]
        public async void NonTyped_To_Typed_Async_Continuation_Infered()
        {
            var result = await Task.FromResult(Result.Ok())
                .ThenAsync(async () => await Task.FromResult(1))
                ;
        }
        [Fact]
        public async void Typed_To_NonTyped_Sync_Continuation()
        {
            var result = await Task.FromResult(Result.Ok(1))
                .ThenAsync((value) => Result.Ok())
                ;
        }
        [Fact]
        public async void Typed_To_NonTyped_Async_Continuation()
        {
            var result = await Task.FromResult(Result.Ok(1))
                .ThenAsync(async (value) => await Task.FromResult(Result.Ok()))
                ;
        }
        [Fact]
        public async void Typed_To_Typed_Sync_Continuation()
        {
            var result = await Task.FromResult(Result.Ok(1))
                .ThenAsync((value) => Result.Ok(string.Empty))
                ;
        }
        [Fact]
        public async void Typed_To_Typed_Async_Continuation()
        {
            var result = await Task.FromResult(Result.Ok(1))
                .ThenAsync(async (value) => await Task.FromResult(Result.Ok(string.Empty)))
                ;
        }
        [Fact]
        public async void Typed_To_Typed_Async_Continuation_Infered()
        {
            var result = await Task.FromResult(Result.Ok(1))
                .ThenAsync(async (value) => await Task.FromResult(string.Empty))
                ;
        }
    } 
}