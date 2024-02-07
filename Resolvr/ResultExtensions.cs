using System;

namespace Resolvr
{
    public static class ResultExt
    {
        public static Tresp Map<T, Tresp>(this Result<T> result, Func<T, Tresp> mapSuccess, Func<Result, Tresp> mapError)
        {
            if(result.IsSuccess) return  mapSuccess.Invoke(result.Value);
            return mapError.Invoke(result.Error);
        }
        public static Tresp Map<Tresp>(this Result result, Func<Tresp> mapSuccess, Func<Result, Tresp> mapError)
        {
            if(result.IsSuccess) return  mapSuccess.Invoke();
            return mapError.Invoke(result.Error);
        }

        public static Result MapError(this Result result, Func<Error, Result> mapError)
        {
            if(result.IsSuccess) return result;
            return mapError.Invoke(result.Error);
        }
        public static Result<T> MapError<T>(this Result<T> result, Func<Error, Result<T>> mapError)
        {
            if(result.IsSuccess) return result;
            return mapError.Invoke(result.Error);
        }


        public static Result MapSuccess(this Result result, Func<Result> mapSuccess)
        {
            if(result.IsError) return result;
            return mapSuccess.Invoke();
        }
        public static Result<T> MapSuccess<T>(this Result<T> result, Func<T, Result<T>> mapSuccess)
        {
            if(result.IsError) return result;
            return mapSuccess.Invoke(result.Value);
        }
        public static Result MapSuccess(this Result result, Action mapSuccess)
        {
            if(result.IsError) return result;
            mapSuccess.Invoke();
            return result;
        }
        public static Result<T> MapSuccess<T>(this Result<T> result, Action<T> mapSuccess)
        {
            if(result.IsError) return result;
            mapSuccess.Invoke(result.Value);
            return result.Value;
        }
    }
}