using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public interface IConnectionProfileRepository
	{
		IList<IConnectionProfile> GetAll();
		void CreateNew(IConnectionProfile profile);
		void Delete(IConnectionProfile profile);
		void Update(IConnectionProfile originalProfile, IConnectionProfile connectionProfile);
	}
}
