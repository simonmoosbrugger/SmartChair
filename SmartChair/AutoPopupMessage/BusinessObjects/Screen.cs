using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AutoPopupMessage.BusinessObjects
{
    public static class Screen
    {
        public static double Width
        {
            get { return SystemParameters.WorkArea.Width; }
        }

        public static double Height
        {
            get { return SystemParameters.WorkArea.Height; }
        }
    }
}
