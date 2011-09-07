namespace Commons.Utils.Delegates
{
	//name is subject to discuss and change
	public delegate void VoidDelegate<T>(T value);
	public delegate void DoubleVoidDelegate<T>(T value1, T value2);
    public delegate void VoidDelegate<T1, T2>(T1 value1, T2 value2);
    public delegate void VoidDelegate<T1, T2,T3>(T1 value1, T2 value2,T3 value3);
    public delegate void VoidDelegate();

	public delegate T FuncDelegate<T, V1>(V1 value);
	public delegate T DoubleFuncDelegate<T, V1>(V1 value1, V1 value2);
	public delegate T FuncDelegate<T>();
}
