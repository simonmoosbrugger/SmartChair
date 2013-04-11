using EARTHLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiimoteLib;

namespace GoogleEarthNew
{
    public partial class Form1 : Form
    {
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

        private ApplicationGE ge = null;

        public Form1()
        {
            foreach (Process p in Process.GetProcessesByName("googleearth"))
            {
                p.Kill();
            }

            InitializeComponent();

            ge = new EARTHLib.ApplicationGE();

            ShowWindowAsync(ge.GetMainHwnd(), 0);
            SetParent(ge.GetRenderHwnd(), this.Handle.ToInt32());

            Wiimote wm = new Wiimote();

            // setup the event to handle state changes
            wm.WiimoteChanged += wm_WiimoteChanged;

            // connect to the Wiimote
            wm.Connect();

            // set the report type to return the IR sensor and accelerometer data (buttons always come back)
            wm.SetReportType(InputReport.IRAccel, true);

            ResizeGoogleControl();
        }

        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            if (e.WiimoteState.BalanceBoardState.CenterOfGravity.Y >= 5)
            {
                Zoom(-zoom, 30);
            }
            else if (e.WiimoteState.BalanceBoardState.CenterOfGravity.Y <= -2)
            {
                Zoom(zoom, 30);
            }


            //if (e.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft > 70 && e.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight > 70)
            //{
            //    Zoom(zoom, 30);
            //}

            //else if (e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft > 70 && e.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight > 70)
            //{
            //    Zoom(-zoom, 30);
            //}
        }

        int zoom = 40000;

        private void Zoom(int dist, int speed)
        {
            CameraInfoGE info = ge.GetCamera(0);
            info.Range += dist;
            ge.SetCamera(info, speed);
        }

        private void ResizeGoogleControl()
        {
            SendMessage(ge.GetMainHwnd(), WM_COMMAND, WM_PAINT, 0);
            PostMessage(ge.GetMainHwnd(), WM_QT_PAINT, 0, 0);

            SetWindowPos(
            ge.GetMainHwnd(),
            HWND_TOP,
            0,
            0,
            (int)this.Width,
            (int)this.Height,
            SWP_FRAMECHANGED);

            SendMessage(ge.GetRenderHwnd(), WM_COMMAND, WM_SIZE, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
