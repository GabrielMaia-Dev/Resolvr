using System;
using System.Threading.Tasks;

namespace Resolvr
{
    public static class ResultTaskExt
    {

        // Task<Result> to Result
        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> task, Func<T, Result<K>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke(result.Value);
        }
        public static async Task<Result<T>> Then<T>(this Task<Result> task, Func<Result<T>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke();
        }
        public static async Task<Result> Then(this Task<Result> task, Func<Result> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke();
        }
        public static async Task<Result> Then<T>(this Task<Result<T>> task, Func<T, Result> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke(result.Value);
        }
        // Task to Object Result
        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> task, Func<T, K> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke(result.Value);
        }
        public static async Task<Result<K>> Then<K>(this Task<Result> task, Func<K> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return then.Invoke();
        }
        public static async Task<Result> Then(this Task<Result> task, Action then)
        {
            var result = await task;
            if(result.IsSuccess) then.Invoke();
            return result;
        }
        public static async Task<Result> Then<T>(this Task<Result<T>> task, Action<T> then)
        {
            var result = await task;
            if(result.IsSuccess) then.Invoke(result.Value);
            return result;
        }

        //Task<Result> to Task<Result>

        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> task, Func<T, Task<Result<K>>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<T>> Then<T>(this Task<Result> task, Func<Task<Result<T>>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke();
        }
        public static async Task<Result> Then(this Task<Result> task, Func<Task<Result>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke();
        }
        public static async Task<Result> Then<T>(this Task<Result<T>> task, Func<T, Task<Result>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke(result.Value);
        }
        // async Task to Object Result
        public static async Task<Result<K>> Then<T, K>(this Task<Result<T>> task, Func<T, Task<K>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<K>> Then<K>(this Task<Result> task, Func<Task<K>> then)
        {
            var result = await task;
            if(result.IsError) return result.Error;
            return await then.Invoke();
        }
        public static async Task<Result> Then(this Task<Result> task, Func<Task> then)
        {
            var result = await task;
            if(result.IsSuccess) await then.Invoke();
            return result;
            
        }
        public static async Task<Result> Then<T>(this Task<Result<T>> task, Func<T, Task> then)
        {
            var result = await task;
            if(result.IsSuccess) await then.Invoke(result.Value);
            return result;
        }
        // non to non
        public static Result Then(this Result result, Func<Result> callback)
        {
            if(result.IsError) return result;
            return callback.Invoke();
        }
        public static Result Then(this Result result, Action action)
        {
            if(result.IsError) return result;
            action.Invoke();
            return Result.Ok();
        }
        public static async Task<Result> Then(this Result result, Func<Task<Result>> callback)
        {
            if(result.IsError) return result;
            return await callback.Invoke();
        }

        // gen to non
        public static Result Then<T>(this Result<T> result, Func<T, Result> callback)
        {
            if(result.IsError) return result;
            return callback.Invoke(result.Value);
        }
        public static async Task<Result> Then<T>(this Result<T> result, Func<T, Task<Result>> callback)
        {
            if(result.IsError) return result;
            return await callback.Invoke(result.Value);
        }

        // non to gen
        public static Result<T> Then<T>(this Result result, Func<Result<T>> callback)
        {
            if(result.IsError) return result.Error;
            return callback.Invoke();
        }
        public async static Task<Result<T>> Then<T>(this Result result, Func<Task<Result<T>>> callback)
        {
            if(result.IsError) return result.Error;
            return await callback.Invoke();
        }

        // gen to gen
        public static Result<K> Then<T, K>(this Result<T> result, Func<T, K> callback)
        {
            if(result.IsError) return result.Error;
            return callback.Invoke(result.Value);
        }
        public static async Task<Result<K>> Then<T, K>(this Result<T> result, Func<T, Task<K>> callback)
        {
            if(result.IsError) return result.Error;
            return Result.Ok(await callback.Invoke(result.Value));
        }

        public static Result<T> Concat<T>(this Result<T> result, Func<T, Result> concat)
        {
            if(result.IsError) return result;
            var nextResult = concat.Invoke(result.Value);
            if(nextResult.IsError) return nextResult.Error;
            return result;
        }
        public static async Task<Result<T>> Concat<T>(this Result<T> result, Func<T, Task<Result>> concat)
        {
            if(result.IsError) return result;
            var nextResult = await concat.Invoke(result.Value);
            if(nextResult.IsError) return nextResult.Error;
            return result;
        }
    }
}