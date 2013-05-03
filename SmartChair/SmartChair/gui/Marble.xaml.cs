using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartChair.gui
{
    /// <summary>
    /// Interaktionslogik für Marble.xaml
    /// </summary>
    public partial class Marble : PageExtended
    {
        public Marble()
        {
            InitializeComponent();
            
        }

        [DllImport("user32.dll")]
        private static extern int SetParent(
        int hWndChild,
        int hWndParent);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(
        int hWnd,
        int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(
        int hWnd,
        uint Msg,
        int wParam,
        int lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern bool SetWindowPos(
        int hWnd,
        int hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

        [DllImport("user32.dll")]
        private static extern int SendMessage(
        int hWnd,
        uint Msg,
        int wParam,
        int lParam);

        private const int HWND_TOP = 0x0;
        private const int WM_COMMAND = 0x0112;
        private const int WM_QT_PAINT = 0xC2DC;
        private const int WM_PAINT = 0x000F;
        private const int WM_SIZE = 0x0005;
        private const int SWP_FRAMECHANGED = 0x0020;
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Process p = Process.Start(@"marble.exe", "-popupwindow");
            Thread.Sleep(500); // Allow the process to open it's window
            HwndSource hwnd = PresentationSource.FromDependencyObject(this) as HwndSource;          

            SetParent(p.MainWindowHandle.ToInt32(), hwnd.Handle.ToInt32());
            ResizeUnityControl(p.MainWindowHandle.ToInt32());

        }

        private void ResizeUnityControl(int handle)
        {
            SendMessage(handle, WM_COMMAND, WM_PAINT, 0);
            PostMessage(handle, WM_QT_PAINT, 0, 0);

            SetWindowPos(
            handle,
            HWND_TOP,
            1,
            85,
            1248,
            594,
            SWP_FRAMECHANGED
            );

            SendMessage(handle, WM_COMMAND, WM_SIZE, 0);
        }

        public bool RemoveListener()
        {
            return false;
        }
    }
}
