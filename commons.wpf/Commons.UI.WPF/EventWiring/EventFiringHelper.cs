using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Commons.UI.WPF.EventWiring
{
	public static class EventFiringHelper
	{
		//MethodImpl set for ensure that no inline for check
		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire(this Action action)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if(action!=null)
			{
				action.Invoke();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire<T1>(this Action<T1> action, T1 arg1)
		{
			if(action!=null)
			{
				action.Invoke(arg1);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire<T1,T2>(this Action<T1,T2> action, T1 arg1, T2 arg2)
		{
			if(action!=null)
			{
				action.Invoke(arg1,arg2);
			}
		}
	
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Fire<T1,T2,T3>(this Action<T1,T2,T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			if(action!=null)
			{
				action.Invoke(arg1,arg2,arg3);
			}
		}
		
		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire<T1,T2,T3,T4>(this Action<T1,T2,T3,T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if(action!=null)
			{
				action.Invoke(arg1,arg2,arg3,arg4);
			}
		}
		
		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire(this PropertyChangedEventHandler handler, object sender, string propertyName)
		{
			if(handler!=null)
			{
				handler.Invoke(sender,new PropertyChangedEventArgs(propertyName));
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire(this EventHandler handler, object sender, EventArgs args)
		{
			if(handler!=null)
			{
				handler.Invoke(sender, args);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)] 
		public static void Fire(this EventHandler handler, object sender)
		{
			if(handler!=null)
			{
				handler.Invoke(sender, new EventArgs());
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static TResult Fire<TResult>(this Func<TResult> func)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if (func != null)
			{
				return func.Invoke();
			}

			return default(TResult);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static TResult Fire<T1,TResult>(this Func<T1,TResult> func, T1 arg1)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if (func != null)
			{
				return func.Invoke(arg1);
			}

			return default(TResult);
		}


		[MethodImpl(MethodImplOptions.NoInlining)]
		public static TResult Fire<T1,T2,TResult>(this Func<T1,T2,TResult> func, T1 arg1, T2 arg2)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if (func != null)
			{
				return func.Invoke(arg1,arg2);
			}

			return default(TResult);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static TResult Fire<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if (func != null)
			{
				return func.Invoke(arg1,arg2,arg3);
			}

			return default(TResult);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static TResult Fire<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			//nevertheless it's probably not thread-safe
			//and intended to use with statefull UI
			if (func != null)
			{
				return func.Invoke(arg1,arg2,arg3,arg4);
			}

			return default(TResult);
		}
	}
}
