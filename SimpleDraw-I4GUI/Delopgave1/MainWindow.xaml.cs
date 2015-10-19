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
using System.Windows.Threading;

namespace Delopgave1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum DrawingTool
    {
        Line,
        Ellipsis,
        Rectangle
    };
    public partial class MainWindow : Window
    {
        private Color _drawColor;
        private Point _startPos;
        private Point _currPos;
        private Shape _currShape;
        private bool isDrawing = false;
        private DrawingTool _tool;
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
          //  KeyUp += new KeyEventHandler(MainWindow_KeyUp);
          //  MouseDown += new MouseButtonEventHandler(MainWindow_MouseDown);
         //   MouseUp += new MouseButtonEventHandler(MainWindow_MouseUp);
            MouseMove += new MouseEventHandler(MainWindow_MouseMove);
          //  SetColor(Colors.Black);
        }

        private void SetColor(Color black)
        {
            throw new NotImplementedException();
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            _currPos = e.GetPosition(canvas);
            tbxX.Text = _currPos.X.ToString("F0");
            tbxY.Text = _currPos.Y.ToString("F0");

            if (!isDrawing) return;
            if (_tool != DrawingTool.Line)
            {
                var width = _currPos.X - _startPos.X;
                var height = _currPos.Y - _startPos.Y;
                _currShape.Width = (width > 1 ? width : 2);
                _currShape.Height = (height > 1 ? height : 2);
            }
            else
            {
                var l = _currShape as Line;
                l.X2 = _currPos.X;
                l.Y2 = _currPos.Y;
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
