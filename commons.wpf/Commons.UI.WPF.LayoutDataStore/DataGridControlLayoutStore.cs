using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore
{
    public class DataGridControlLayoutStore : WPFLayoutDataStore
    {
        private const string FormatFind = "HeaderColumn={0}\r\n";
        private readonly IList<DataGridColumn> columns;
        private DataGridColumnHeader columnHeader;

        public DataGridControlLayoutStore(DataGrid entity, Control parent)
            : base(entity, parent)
        {
            columns = entity.Columns;
            checkCode = GenerateCheckCode(GetDataForCheckCode());
        }

        private string GetDataForCheckCode()
        {
            return Save();
        }

        public override void SetUI4LoadDefault(ILayoutStoreWorker lsworker)
        {
            base.SetUI4LoadDefault(lsworker);
            DataGrid grid = (DataGrid) entity;
            ContextMenu menu = grid.ContextMenu;
            if (menu == null)
                menu = new ContextMenu();
            menu.Items.Add(GetLoadDefaultMenuItem());
            grid.ContextMenu = menu;
            grid.MouseRightButtonDown += grid_MouseRightButtonDown;
        }

        private void grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject) e.OriginalSource;
            columnHeader = null;
            // iteratively traverse the visual tree
            while ((dep != null) &&
                   !(dep is DataGridCell) &&
                   !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                columnHeader = dep as DataGridColumnHeader;
                ContextMenu menu = columnHeader.ContextMenu;
                if (menu == null)
                    menu = new ContextMenu();
                MenuItem loadDefaultMenuItem = GetLoadDefaultMenuItem();
                if (!ContainsItem(menu, loadDefaultMenuItem))
                    menu.Items.Add(loadDefaultMenuItem);
                MenuItem hideMenuItem = new MenuItem {Header = Properties.Resources.DataGridHideColumn, Name = "hideMenuItem"};
                hideMenuItem.Click += hideMenuItem_Click;
                if (!ContainsItem(menu, hideMenuItem))
                    menu.Items.Add(hideMenuItem);
                columnHeader.ContextMenu = menu;
                // do something
            }

/*
            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                // do something
            }
*/
        }

        private static bool ContainsItem(ItemsControl menu, IFrameworkInputElement menuItem)
        {
            foreach (MenuItem item in menu.Items)
            {
                if (item.Name == menuItem.Name)
                    return true;
            }
            return false;
        }

        private void hideMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (columnHeader != null)
                DataGridColumnSettings.UnvisibleColumn(columnHeader.Column);
        }

        private MenuItem GetLoadDefaultMenuItem()
        {
            MenuItem itemLoadDefault = new MenuItem {Header = Properties.Resources.DataGridLoadDefault, Name = "itemLoadDefault"};
            itemLoadDefault.Click += itemLoadDefault_Click;
            return itemLoadDefault;
        }

        private void itemLoadDefault_Click(object sender, RoutedEventArgs e)
        {
            worker.LoadDefault(this);
        }

        protected override void SaveEntity2Memory(Stream memory)
        {
            if (columns != null)
                for (int i = 0; i < columns.Count; i++)
                {
                    DataGridColumn column = columns[i];
                    Stream tempMemory = SaveLayoutToStream(column, i);
                    string str = LayoutStoreUtils.Stream2String(tempMemory);
                    tempMemory.Seek(0, SeekOrigin.Begin);
					tempMemory = LayoutStoreUtils.String2Stream(
                        string.Format("\r\n" + FormatFind + "{1}", column.Header, str));
                    StreamWriter writer = new StreamWriter(memory);
                    writer.AutoFlush = true;
					writer.Write(LayoutStoreUtils.Stream2String(tempMemory));
                }
        }

        private Stream SaveLayoutToStream(DataGridColumn column, int index)
        {
            Stream memory = new MemoryStream();
            DataGridColumnSettings.Save(index, column, memory);
            return memory;
        }

        protected override void LoadEntityFromString(string xmlLayoutData)
        {
            const string endInfo = "</DataGridColumnSettings>";
            foreach (DataGridColumn column in columns)
            {
                string strFind = string.Format(FormatFind, column.Header);

                int posBegin = xmlLayoutData.IndexOf(strFind);
                if (posBegin > 0)
                {
                    posBegin += strFind.Length;
                    string strData = xmlLayoutData.Substring(posBegin);
                    int posEnd = strData.IndexOf(endInfo);
                    if (posEnd > 0)
                        posEnd += endInfo.Length;
                    else
                        throw new LayoutDataStoreException("Не найдено окончание данных");

                    strData = strData.Substring(0, posEnd);
					DataGridColumnSettings.Restore(column, LayoutStoreUtils.String2Stream(strData));
                }
                else
                    throw new LayoutDataStoreException("Не найдено начало данных");
            }
        }

        protected void LoadByDefault(object sender, EventArgs e)
        {
            if (worker != null)
                worker.LoadDefault(this);
        }
    }
}
