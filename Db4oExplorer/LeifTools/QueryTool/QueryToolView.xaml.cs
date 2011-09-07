using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Db4oExplorer.Domain;
using ICSharpCode.AvalonEdit.Highlighting;
using Commons.UI.WPF.EventWiring;

namespace LeifTools.QueryTool
{
	/// <summary>
	/// Interaction logic for QueryToolView.xaml
	/// </summary>
	public partial class QueryToolView : UserControl
	{
		public QueryToolView()
		{
			InitializeComponent();

			//in current build of AvalonEdit no python highlighter - but Boo is much the same
			queryTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Boo");
			openButton.Click += (s,e) => OpenFired.Fire();
			saveButton.Click += (s,e) => SaveFired.Fire(QueryText);
		}

		public event Action<string> ExecuteFired;
		public event Action OpenFired;
		public event Action<string> SaveFired;

		public IList Result
		{
			set
			{
				dataView.DataSource = value;
			}
		}

		public IList<Field> Fields
		{

			set { dataView.Fields = value; }
		}

		public string QueryText
		{
			set { queryTextBox.Text = value; }
			get { return queryTextBox.Text; }
		}

		private void executeButton_Click(object sender, RoutedEventArgs e)
		{
			ExecuteFired.Fire(QueryText);
		}
	}
}


