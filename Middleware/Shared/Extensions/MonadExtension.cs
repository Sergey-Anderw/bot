﻿namespace Shared.Extensions
{
    public static class MonadExtension
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null)
                return null!;

            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
        {
            if (o == null)
                return default(TResult)!;

            return evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (o == null)
                return failureValue;

            return evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) 
            where TInput : class
        {
            if (o == null)
                return null!;

            return (evaluator(o) ? o : null)!;
        }

        public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator) 
            where TInput : class
        {
            if (o == null)
                return null!;

            return (evaluator(o) ? null : o)!;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action) 
            where TInput : class
        {
            if (o == null)
                return null!;
            
            action(o);
            
            return o;
        }
    }
}