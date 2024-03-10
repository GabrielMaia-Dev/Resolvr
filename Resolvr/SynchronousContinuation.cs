using System;

namespace Resolvr.Continuation
{
    public static class SynchronousContinuationExt
    {
        
        #region Synchronous Continuation
        public static Result Then(this Result result, Func<Result> then)
        {
            if(result.IsError) return result;
            return then.Invoke();
        }

        public static Result<T> Then<T>(this Result result, Func<Result<T>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return then.Invoke();
        }
        public static Result Then<T>(this Result<T> result, Func<T, Result> then)
        {
            if(result.IsError) return result.ErrorValue;
            return then.Invoke(result.Value);
        }
        public static Result<K> Then<T, K>(this Result<T> result, Func<T, Result<K>> then)
        {
            if(result.IsError) return result.ErrorValue;
            return then.Invoke(result.Value);
        }
        public static Result Then(this Result result, Action action)
        {
            if(result.IsError) return result;
            action.Invoke();
            return Result.Ok();
        }
        public static Result Then<T>(this Result<T> result, Action<T> action)
        {
            if(result.IsError) return result.ErrorValue;
            action.Invoke(result.Value);
            return Result.Ok();
        }
        #endregion
    }
}