using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Resolvr.Continuation
{
    public static class AsynchronousContinuationExt
    {
        public static async Task<Result> ThenAsync(this Result result, Func<Task<Result>> then)
        {
            if(result.IsError) return result;
            return await then.Invoke();
        }
        public static async Task<Result> ThenAsync<T>(this Result<T> result, Func<T, Task<Result>> then)
        {
            if(result.IsError) return result;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<T>> ThenAsync<T>(this Result result, Func<Task<Result<T>>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke();
        }
        public static async Task<Result<T>> ThenAsync<T>(this Result result, Func<Task<T>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke();
        }
        public static async Task<Result<K>> ThenAsync<T, K>(this Result<T> result, Func<T, Task<Result<K>>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<K>> ThenAsync<T, K>(this Result<T> result, Func<T, Task<K>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result> ThenAsync(this Task<Result> task, Func<Task<Result>> then)
        {
            var result = await task;
            if(result.IsError) return result;
            return await then.Invoke();
        }

        public static async Task<Result> ThenAsync(this Task<Result> task, Func<Result> then)
        {
            var result = await task;
            if(result.IsError) return result;
            return then.Invoke();
        }
        public static async Task<Result> ThenAsync<T>(this Task<Result<T>> task, Func<T, Task<Result>> then)
        {
            var result = await task;
            if(result.IsError) return result;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result> ThenAsync<T>(this Task<Result<T>> task, Func<T, Result> then)
        {
            var result = await task;
            if(result.IsError) return result;
            return then.Invoke(result.Value);
        }
        public static async Task<Result<T>> ThenAsync<T>(this Task<Result> task, Func<Task<Result<T>>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke();
        }
        public static async Task<Result<T>> ThenAsync<T>(this Task<Result> task, Func<Task<T>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke();
        }
        public static async Task<Result<T>> ThenAsync<T>(this Task<Result> task, Func<Result<T>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return then.Invoke();
        }
        public static async Task<Result<K>> ThenAsync<T, K>(this Task<Result<T>> task, Func<T, Task<Result<K>>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<K>> ThenAsync<T, K>(this Task<Result<T>> task, Func<T, Task<K>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return await then.Invoke(result.Value);
        }
        public static async Task<Result<K>> ThenAsync<T, K>(this Task<Result<T>> task, Func<T, Result<K>> then)
        {
            var result = await task;
            if(result.IsError) return result.ErrorValue;
            return then.Invoke(result.Value);
        }
    }
}