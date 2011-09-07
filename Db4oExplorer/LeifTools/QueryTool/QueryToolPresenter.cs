using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Commons.UI.WPF;
using System.Linq;
using Commons.UI.WPF.Services;

namespace LeifTools.QueryTool
{
	public class QueryToolPresenter
	{
		private const string FILTER = "Python object manipulation language file(*.pyoml)|*.pyoml";
		private readonly QueryToolView queryToolView;
		private readonly IWindowManager windowManager;
		private ScriptEngine scriptEngine = Python.CreateEngine();
		private ScriptScope scriptScope = null;
		private ObservableCollection<ConnectionViewModel> connectionViewModels;
		private readonly IBrowseFileService browseFileService;

		public QueryToolPresenter(QueryToolView queryToolView, 
			ObservableCollection<ConnectionViewModel> connectionViewModels,
			IBrowseFileService browseFileService)
		{
			this.queryToolView = queryToolView;
			this.connectionViewModels = connectionViewModels;
			this.browseFileService = browseFileService;
			queryToolView.ExecuteFired += new System.Action<string>(Execute);
			queryToolView.SaveFired += new Action<string>(Save);
			queryToolView.OpenFired += new Action(Open);
		}

		void Open()
		{
			BrowseResult browse = browseFileService.Browse(new BrowseParams() {Filter = FILTER});

			if(browse.Cancel) return;

			queryToolView.QueryText = File.ReadAllText(browse.FilePath);
		}

		private void Save(string queryText)
		{
			BrowseResult result = browseFileService.Save(new BrowseParams(){Filter = FILTER});
			if(result.Cancel)
				return;

			File.WriteAllText(result.FilePath,queryText);
		}

		void Execute(string queryString)
		{
			ScriptSource scriptSource = scriptEngine.CreateScriptSourceFromString(queryString, SourceCodeKind.Statements);
			ScriptScope scope = scriptEngine.CreateScope();
			ConnectionViewModel connectionViewModel = connectionViewModels.First(c => c.IsConnected);

			var queryObject = new QueryObject(connectionViewModel.Connection);
			scope.SetVariable("qo",queryObject);

			object result = scriptSource.Execute(scope);

			if (result != null) MessageBox.Show(result.ToString());

			object data = null;
			scope.TryGetVariable("data", out data);
			if(data!=null)
			{
//				var storedClass = query as IStoredClass;

				queryToolView.Fields = queryObject.StoredClass.Fields;
				queryToolView.Result = (IList) data;
			}

//			if (query != null) MessageBox.Show(query.ToString());

//			PythonDictionary pythonDic = (PythonDictionary) scope.GetVariable("__builtins__");
//
//			foreach (var pair in pythonDic)
//			{
//				Console.WriteLine(pair.Key + "=" + pair.Value);
//			}
//
//			foreach (var variableName in scope.GetVariableNames())
//			{
//				Console.WriteLine(variableName);
//			}

//			object listResult = null;
//			scriptEngine.TryGetVariable(scope, "_", out listResult);
//			if (listResult != null) MessageBox.Show(listResult.ToString());


		}
	}
}
