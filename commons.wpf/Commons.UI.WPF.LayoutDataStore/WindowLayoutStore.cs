using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF.LayoutDataStore.Properties;

namespace Commons.UI.WPF.LayoutDataStore
{
    public class WindowLayoutStore : WPFLayoutDataStore
    {
        #region Win32 API Stuff 

        // Define the Win32 API methods we are going to use
        private const Int32 MF_BYPOSITION = 0x400;
        private const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_STRING = 0x0;

        /// Define our Constants we will use
        private const Int32 WM_SYSCOMMAND = 0x112;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem,
                                              string lpNewItem);

        #endregion

        // The constants we'll use to identify our custom system menu items
        private const Int32 DefaultSysMenuId = 1000;

        public WindowLayoutStore(Control window) : base(window, window.Name, false)
        {
            checkCode = GenerateCheckCode(GetDataForCheckCode());
        }

        /// This is the Win32 Interop Handle for this Window
        /// </summary>
        private IntPtr Handle
        {
            get { return new WindowInteropHelper((Window) entity).Handle; }
        }

        public override void SetUI4LoadDefault(ILayoutStoreWorker lsworker)
        {
            worker = lsworker;
            //http://pietschsoft.com/post/2008/03/Add-System-Menu-Items-to-WPF-Window-using-Win32-API.aspx      
            ((Window) entity).Loaded += WindowLayoutStore_Loaded;
        }

        private void WindowLayoutStore_Loaded(object sender, RoutedEventArgs e)
        {
            /// Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(Handle, false);

            /// Create our new System Menu items just before the Close menu item
            InsertMenu(systemMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty); // <-- Add a menu seperator
            InsertMenu(systemMenuHandle, 6, MF_BYPOSITION, DefaultSysMenuId, Resources.DefaultSettings);

            // Attach our WndProc handler to this Window
            HwndSource source = HwndSource.FromHwnd(Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Check if a System Command has been executed
            if (msg == WM_SYSCOMMAND)
            {
                // Execute the appropriate code for the System Menu item that was clicked
                if (wParam.ToInt32() == DefaultSysMenuId)
                {
                    if (worker != null)
                        worker.LoadDefault(this);
                    ((Window) entity).WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    handled = true;
                }
            }

            return IntPtr.Zero;
        }

        protected string GetDataForCheckCode()
        {
            Window window = (Window) entity;
            Point location = new Point {X = window.Left, Y = window.Top};
            string localCheckCode = Save();
            window.Left = location.X;
            window.Top = location.Y;
            return localCheckCode;
        }

        protected override void SaveEntity2Memory(Stream memory)
        {
            WindowSettings.Save((Window) entity, memory);
        }

        protected override void LoadEntityFromString(string xmlLayoutData)
        {
            Window window = (Window) entity;
            Stream str = UI.LayoutDataStore.LayoutStoreUtils.String2Stream(xmlLayoutData);
            str.Position = 0;

            WindowSettings.Restore(window, str);
        }
    }
}
