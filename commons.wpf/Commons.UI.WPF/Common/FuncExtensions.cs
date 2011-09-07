using System;

namespace Commons.UI.WPF.Common
{
	public static class FuncExtensions
	{
		public static Predicate<TArg2> Apply<TArg1, TArg2>(this Func<TArg1, TArg2, bool> func, TArg1 arg1)
		{
			return arg2 => func(arg1, arg2);
		}
	
		public static Action Apply<TArg1>(this Action<TArg1> action, TArg1 arg1)
		{
			return () => action(arg1);
		}
	
		public static Action Apply<TArg1,TArg2>(this Action<TArg1,TArg2> action, TArg1 arg1, TArg2 arg2)
		{
			return () => action(arg1,arg2);
		}
	
		public static Action Apply<TArg1,TArg2,TArg3>(this Action<TArg1,TArg2,TArg3> action, TArg1 arg1, TArg2 arg2, TArg3 arg3)
		{
			return () => action(arg1,arg2,arg3);
		}
		
		public static Action Apply<TArg1,TArg2,TArg3,TArg4>(this Action<TArg1,TArg2,TArg3,TArg4> action, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
		{
			return () => action(arg1,arg2,arg3,arg4);
		}

		public static Func<TArg1, Func<TArg2, TResult>> Curry<TArg1, TArg2, TResult>(
			this Func<TArg1, TArg2, TResult> func)
		{
			return arg1 => arg2 => func(arg1, arg2);
		}

		public static Func<TArg2, TResult> Apply<TArg1, TArg2, TResult>(
			this Func<TArg1, TArg2, TResult> func, TArg1 arg1)
		{
			return arg2 => func(arg1, arg2);
		}

		public static Func<TSource, TResult> ForwardCompose<TSource, TIntermediate, TResult>(
			this Func<TSource, TIntermediate> func1, Func<TIntermediate, TResult> func2)
		{
			return source => func2(func1(source));
		}
	}
}
