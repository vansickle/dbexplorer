using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Xml.Serialization;

namespace Commons.UI.WPF.LayoutDataStore
{
    [Serializable]
    public class WindowSettings : IWindowSettings
    {
        private Size size;

        #region IWindowSettings Members

        public Size ContentSize { get; set; }

        public Point Location { get; set; }

        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                if (double.IsNaN(size.Width))
                    size.Width = 300;
                if (double.IsNaN(size.Height))
                    size.Height = 300;
            }
        }

        public WindowState State { get; set; }

        #endregion

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, ref Rect rect);

        private Rect GetWindowRect(Window window)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            Rect rect = new Rect();

            GetClientRect(hwnd, ref rect);
            return rect;
        }

        private void AssignTo(Window window)
        {
            window.Width = size.Width;
            window.Height = size.Height;

            if (window.HasContent)
            {
                FrameworkElement root = window.Content as FrameworkElement;
                if (root != null)
                {
                    window.SizeToContent = SizeToContent.Manual;
                    root.Width = ContentSize.Width;
                    root.Height = ContentSize.Height;
                }
            }

            Point loc = Location;
            bool locIsNaN = (double.IsNaN(loc.X) && double.IsNaN(loc.Y));

            // If the user's machine had two monitors but now only
            // has one, and the Window was previously on the other
            // monitor, we need to move the Window into view.
            bool outOfBounds =
                loc.X <= -size.Width ||
                loc.Y <= -size.Height ||
                SystemParameters.VirtualScreenWidth <= loc.X ||
                SystemParameters.VirtualScreenHeight <= loc.Y;

            if (double.IsNaN(loc.X)) loc.X = 0;
            if (double.IsNaN(loc.Y)) loc.Y = 0;

            if (outOfBounds || locIsNaN)
            {
                window.WindowStartupLocation = WindowStartupLocation.Manual;
                window.Left = SystemParameters.WorkArea.Left +
                              (SystemParameters.WorkArea.Width - size.Width)/2;
                window.Top = SystemParameters.WorkArea.Top
                             + (SystemParameters.WorkArea.Height - size.Height)/2;
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.Manual;
                window.Left = loc.X;
                window.Top = loc.Y;

                // We need to wait until the HWND window is initialized before
                // setting the state, to ensure that this works correctly on
                // a multi-monitor system.  Thanks to Andrew Smith for this fix.
            }
            window.SourceInitialized += delegate { window.WindowState = State; };
        }

        private void AssignFrom(Window window)
        {
            Rect rect = GetWindowRect(window);
            Location = new Point {X = window.Left, Y = window.Top};
            Size = new Size {Width = window.Width, Height = window.Height};
            ContentSize = GetContentSize(window);
            State = window.WindowState;
        }

        private static Size GetContentSize(ContentControl window)
        {
            FrameworkElement root = window.Content as FrameworkElement;
            return root != null
                       ?
                           new Size {Width = root.Width, Height = root.Height}
                       :
                           new Size {Width = double.NaN, Height = double.NaN};
        }

        public static void Save(Window window, string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                WindowSettings settings = new WindowSettings();
                settings.AssignFrom(window);
                formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                formatter.Serialize(stream, settings);
                stream.Close();
            }
        }

        public static void Save(Window window, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            WindowSettings settings = new WindowSettings();
            settings.AssignFrom(window);

            XmlSerializer serializer = new XmlSerializer(typeof (WindowSettings));
            serializer.Serialize(stream, settings);
        }

        public static void Restore(Window window, string fileName)
        {
            if (!File.Exists(fileName)) return;
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                WindowSettings settings = (WindowSettings) formatter.Deserialize(stream);
                if (settings != null)
                    settings.AssignTo(window);
            }
        }

        public static void Restore(Window window, Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            XmlSerializer serializer = new XmlSerializer(typeof (WindowSettings));
            WindowSettings settings = (WindowSettings) serializer.Deserialize(stream);
            if (settings != null)
                settings.AssignTo(window);
        }
    }
}
