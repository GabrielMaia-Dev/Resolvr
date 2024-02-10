using System;
using System.Collections.Generic;
using Resolvr;
using Xunit;

namespace Test
{
    public class ResultExtensionsTest
    {
        public static IEnumerable<object[]> AnyResult()
        {
            foreach (var data in TypedResults()) yield return data;
            foreach (var data in NonTypedResults()) yield return data;
        }

        public static IEnumerable<object[]> TypedResults()
        {
            yield return new object[] { Result.Ok(1), true };
            yield return new object[] { Result.Error<int>(new Error("Error Message")), false };
            yield return new object[] { Result.Error<int>("Error Message"), false };
            yield return new object[] { new Error(), false };
        }

        public static IEnumerable<object[]> NonTypedResults()
        {
            yield return new object[] { Result.Ok(), true };
            yield return new object[] { new Error(), false };
            yield return new object[] { Result.Error(), false };
        }

        [Theory]
        [MemberData(nameof(AnyResult))]
        public void Result_Match(Result result, bool isSuccess)
        {
            bool executed = false;

            Action success = () => {
                executed = true;
                Assert.True(isSuccess);
            };

            Action<Error> error  = (error) => {
                executed = true;
                Assert.False(isSuccess);
                Assert.True(result.IsError);
                Assert.Equal(result.ErrorValue, error);
                Assert.Equal(result.ErrorValue.Message, error.Message);
            };

            result.Match(success, error);

            Assert.True(executed);
        }
        [Theory]
        [MemberData(nameof(TypedResults))]
        public void Result_Match_Typed(object obj, bool isSuccess)
        {
            bool executed = false;
            Result<int> result = MapTo<int>(obj);

            Action success = () => {
                executed = true;
                Assert.True(isSuccess);
            };

            Action<Error> error  = (error) => {
                executed = true;
                Assert.False(isSuccess);
                Assert.True(result.IsError);
                Assert.Equal(result.ErrorValue, error);
                Assert.Equal(result.ErrorValue.Message, error.Message);
            };

            result.Match(success, error);

            Assert.True(executed);
        }

        [Theory]
        [MemberData(nameof(AnyResult))]
        public void Result_Map(Result result, bool isSuccess)
        {
            bool executed = false;
            Func<bool> success  = () => {
                executed = true;
                return true;
            };
            Func<Error, bool> error  = (error) => {
                executed = true;
                Assert.True(result.IsError);
                Assert.Equal(result.ErrorValue, error);
                Assert.Equal(result.ErrorValue.Message, error.Message);
                return false;
            };

            var mapped = result.Map(success, error);

            Assert.Equal(isSuccess, mapped);
            Assert.True(executed);
        }

        [Theory]
        [MemberData(nameof(TypedResults))]
        public void Result_Map_Typed(object obj, bool isSuccess)
        {
            bool executed = false;
            Result<int> result = MapTo<int>(obj);

            Func<bool> success  = () => {
                executed = true;
                return true;
            };

            Func<Error, bool> error  = (error) => {
                executed = true;
                Assert.True(result.IsError);
                Assert.Equal(result.ErrorValue, error);
                Assert.Equal(result.ErrorValue.Message, error.Message);
                return false;
            };

            var mapped = result.Map(success, error);

            Assert.Equal(isSuccess, mapped);
            Assert.True(executed);
        }

        [Theory]
        [MemberData(nameof(TypedResults))]
        public void Unwrap(object obj, bool isSuccess)
        {
            var result = MapTo<int>(obj);

            if(isSuccess)
            {
                var value = result.Unwrap();
                Assert.True(value != 0);
            }
            else
            {
                Assert.Throws<UnwrapException>(() =>
                {
                    result.Unwrap();
                });
            }
        }
        [Theory]
        [MemberData(nameof(AnyResult))]
        public void UnwrapError(Result result, bool isSuccess)
        {
            if (isSuccess)
            {
                Assert.Throws<UnwrapException>(() =>
                {
                    result.UnwrapError();
                });
            }
            else
            {
                var error = result.UnwrapError();
                Assert.NotNull(error);
            }
        }

        [Theory]
        [MemberData(nameof(AnyResult))]
        public void OnSuccess(Result result, bool isSuccess)
        {
            bool executed = false;

            result.OnSuccess(() => executed = true);

            if(isSuccess) Assert.True(executed);
            else Assert.False(executed);
        }

        [Theory]
        [MemberData(nameof(TypedResults))]
        public void OnSuccess_Typed(object obj, bool isSuccess)
        {
            bool executed = false;

            var result = MapTo<int>(obj);

            result.OnSuccess((v) =>
            {
                executed = true;
                Assert.True(v != 0);
            });

            if(isSuccess) Assert.True(executed);
            else Assert.False(executed);
        }

        [Theory]
        [MemberData(nameof(AnyResult))]
        public void OnError(Result result, bool isSuccess)
        {
            bool executed = false;

            result.OnError(() => executed = true);

            if(isSuccess) Assert.False(executed);
            else Assert.True(executed);
        }

        [Theory]
        [MemberData(nameof(TypedResults))]
        public void OnError_Typed(object obj, bool isSuccess)
        {
            bool executed = false;

            var result = MapTo<int>(obj);

            result.OnError((v) =>
            {
                executed = true;
                Assert.NotNull(v);
            });

            if(isSuccess) Assert.False(executed);
            else Assert.True(executed);
        }

        [Theory]
        [MemberData(nameof(AnyResult))]
        public void Catch_On_Error(Result result, bool isSuccess)
        {
            var executed = false;
            var successResult =  result.Catch((error) =>
            {
                executed = true;
                return Result.Ok();
            });

            if(isSuccess == false) Assert.True(executed);
            Assert.True(successResult.IsSuccess);
        }
        [Theory]
        [MemberData(nameof(TypedResults))]
        public void Catch_On_Error_Typed(object obj, bool isSuccess)
        {
            Result<int> result = MapTo<int>(obj);
            var executed = false;
            var successResult =  result.Catch((error) =>
            {
                executed = true;
                return Result.Ok();
            });

            if(isSuccess == false) Assert.True(executed);
            Assert.True(successResult.IsSuccess);
        }

        private static Result<T> MapTo<T>(object obj)
        {
            Result<T> result;
            if (obj.GetType().IsAssignableTo(typeof(Result<int>)))
            {
                result = (obj as Result<T>)!;
            }
            else if (obj.GetType().IsAssignableTo(typeof(Error)))
            {
                result = (obj as Error)!;
            }
            else throw new Exception();

            return result;
        }
    }

}