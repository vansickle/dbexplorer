using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Windows.Controls;

namespace Commons.UI.WPF.LayoutDataStore
{

    [Serializable]
    public class DataGridColumnSettings 
    {
        public int DisplayIndex { get; set; }
        public double Width { get; set; }
        public double MinWidth { get; set; }
        public bool Visible { get; set; }
        public DataGridLengthUnitType LengthUnitType { get; set; }

        private void AssignTo(DataGridColumn column)
        {
            if (Visible)
            {
                column.Width = new DataGridLength(Width,LengthUnitType);
                column.MinWidth = MinWidth;
            }
            else
            {
                UnvisibleColumn(column);
            }
            column.DisplayIndex = DisplayIndex;
        }

        public static void UnvisibleColumn(DataGridColumn column)
        {
            column.Width = new DataGridLength(0);
            column.MinWidth = 0;
        }

        private void AssignFrom(DataGridColumn column, int index)
        {
            Visible = (column.Width.Value > 0);

            DisplayIndex = column.DisplayIndex == -1 ? index : column.DisplayIndex;
            Width = column.Width.Value;
            MinWidth = column.MinWidth;
            LengthUnitType = column.Width.UnitType;
        }

        public static void Save(int index, DataGridColumn column, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            DataGridColumnSettings settings = new DataGridColumnSettings();
            settings.AssignFrom(column, index);

            XmlSerializer serializer = new XmlSerializer(typeof (DataGridColumnSettings));
            serializer.Serialize(stream, settings);
        }

        public static void Restore(DataGridColumn column, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (column == null) throw new ArgumentNullException("column");

            XmlSerializer serializer = new XmlSerializer(typeof (DataGridColumnSettings));
            DataGridColumnSettings settings = (DataGridColumnSettings) serializer.Deserialize(stream);
            if (settings != null)
                settings.AssignTo(column);
        }

    }
}
