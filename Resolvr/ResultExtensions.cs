using System;

namespace Resolvr
{
    public static class ResultExt
    {
        #region Mapping
        public static Tresp Map<T, Tresp>(this Result<T> result, Func<T, Tresp> mapSuccess, Func<Error, Tresp> mapError)
        {
            if(result.IsSuccess) return  mapSuccess.Invoke(result.Value);
            return mapError.Invoke(result.ErrorValue);
        }
        public static Tresp Map<Tresp>(this Result result, Func<Tresp> mapSuccess, Func<Error, Tresp> mapError)
        {
            if(result.IsSuccess) return  mapSuccess.Invoke();
            return mapError.Invoke(result.ErrorValue);
        }
        #endregion

        #region Error Handling
        public static Result Catch(this Result result, Func<Error, Result> mapError)
        {
            if(result.IsSuccess) return result;
            return mapError.Invoke(result.ErrorValue);
        }
        public static Result<T> Catch<T>(this Result<T> result, Func<Error, Result<T>> mapError)
        {
            if(result.IsSuccess) return result;
            return mapError.Invoke(result.ErrorValue);
        }
        #endregion

        #region Conditional Execution
        public static void OnSuccess(this Result result, Action onSuccess)
        {
            if(result.IsSuccess) onSuccess.Invoke();
        }
        public static void OnSuccess<T>(this Result<T> result, Action<T> onSuccess)
        {
            if(result.IsSuccess) onSuccess.Invoke(result.Value);
        }
        public static void OnError(this Result result, Action mapSuccess)
        {
            if(result.IsError) mapSuccess.Invoke();
        }
        public static void OnError<T>(this Result<T> result, Action<Error> mapSuccess)
        {
            if(result.IsError)  mapSuccess.Invoke(result.ErrorValue);
        }
        public static void Match(this Result result, Action mapSuccess, Action<Error> mapError)
        {
            if(result.IsSuccess)  mapSuccess.Invoke();
            else mapError.Invoke(result.ErrorValue);
        }
        public static void Match<T>(this Result<T> result, Action<T> mapSuccess, Action<Error> mapError)
        {
            if(result.IsSuccess)  mapSuccess.Invoke(result.Value);
            else mapError.Invoke(result.ErrorValue);
        }
        #endregion
        
        #region Execute Or Throw
        public static T Unwrap<T>(this Result<T> result, Exception? exception = null)
        {
            if(result.IsSuccess) return result.Value;
            var ex = exception ?? new UnwrapException("Expected a success result.");
            throw ex; 
        }
        public static Error UnwrapError(this Result result, Exception? exception = null)
        {
            if(result.IsError) return result.ErrorValue;
            var ex = exception ?? new UnwrapException("Expected a error result."); 
            throw ex; 
        }
        #endregion
    }
}