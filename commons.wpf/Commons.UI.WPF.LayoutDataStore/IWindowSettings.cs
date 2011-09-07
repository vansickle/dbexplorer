using System.Windows;

namespace Commons.UI.WPF.LayoutDataStore
{
    public interface IWindowSettings
    {
        Point Location { get; set; }
        Size Size { get; set; }
        WindowState State { get; set; }
        Size ContentSize { get; set; }
    }
}
