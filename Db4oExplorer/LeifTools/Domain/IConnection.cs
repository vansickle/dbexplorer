using System.Collections;
using System.Collections.Generic;

namespace Db4oExplorer.Domain
{
	public interface IConnection
	{
		IConnectionProfile Profile { get; set; }
		string Name { get; }
		string Path { get; }
		IList<IStoredClass> Objects { get; }
		bool IsConnected { get; }
		ConnectionStatus Status { get; set; }
		IList<NameValue> Statistics { get; }
		void UpdateProfileBy(IConnectionProfile profile);
		void Disconnect();
		void TryConnect();
		void Defragment();
		void CreateNewClass();
		IList GetData(IStoredClass storedClass);
		IList GetData(string className);
	}
}
