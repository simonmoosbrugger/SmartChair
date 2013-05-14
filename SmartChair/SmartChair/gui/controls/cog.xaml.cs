using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartChair.gui.controls
{
    /// <summary>
    /// Interaktionslogik für cog.xaml
    /// </summary>
    public partial class cog : UserControl
    {
        private double _maxValue = 5;
        private Point _centerPoint1CoordX;
        private Point _centerPoint1CoordY;
        private Point _centerPoint2CoordX;
        private Point _centerPoint2CoordY;

        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        private double midX, midY;

        public cog(double height, double width)
        {
            InitializeComponent();

            this.Height = height;
            this.Width = width;

            midX = this.Width / 2;
            midY = this.Height / 2;
            
            L1.Y1 = 0;
            L1.Y2 = this.Height;
            L1.X1 = midX;
            L1.X2 = midX;

            L2.Y1 = midY;
            L2.Y2 = midY;
            L2.X1 = 0;
            L2.X2 = this.Width;

            CenterPoint.X1 = midX + 10;
            CenterPoint.X2 = midX - 10;
            CenterPoint.Y1 = midY;
            CenterPoint.Y2 = midY;

            CenterPoint2.X1 = midX;
            CenterPoint2.X2 = midX;
            CenterPoint2.Y1 = midY + 10;
            CenterPoint2.Y2 = midY - 10;

            _centerPoint1CoordX = new Point(CenterPoint.X1, CenterPoint.X2);
            _centerPoint1CoordY = new Point(CenterPoint.Y1, CenterPoint.Y2);
            _centerPoint2CoordX = new Point(CenterPoint2.X1, CenterPoint2.X2);
            _centerPoint2CoordY = new Point(CenterPoint2.Y1, CenterPoint2.Y2);
        }

        public void setPoint(double x, double y)
        {
            x = midX * x / _maxValue;
            y = midY * y / _maxValue;
            CenterPoint.X1 = _centerPoint1CoordX.X + x;
            CenterPoint.X2 = _centerPoint1CoordX.Y + x;
            CenterPoint.Y1 = _centerPoint1CoordY.X + y;
            CenterPoint.Y2 = _centerPoint1CoordY.Y + y;

            CenterPoint2.X1 = _centerPoint2CoordX.X + x;
            CenterPoint2.X2 = _centerPoint2CoordX.Y + x;
            CenterPoint2.Y1 = _centerPoint2CoordY.X + y;
            CenterPoint2.Y2 = _centerPoint2CoordY.Y + y;
        }
    }
}
