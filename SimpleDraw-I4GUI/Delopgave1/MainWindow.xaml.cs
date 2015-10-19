using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
        private bool _isDrawing = false;
        private DrawingTool _tool;
        public MainWindow()
        {
            InitializeComponent();
            KeyUp += new KeyEventHandler(MainWindow_KeyUp);
            MouseDown += new MouseButtonEventHandler(MainWindow_MouseDown);
            MouseUp += new MouseButtonEventHandler(MainWindow_MouseUp);
            MouseMove += new MouseEventHandler(MainWindow_MouseMove);
            SetColor(Colors.Black);
        }

        private void SetColor(Color color)
        {
            _drawColor = color;
            rctColor.Fill = new SolidColorBrush(_drawColor);
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            _currPos = e.GetPosition(canvas);
            tbxX.Text = _currPos.X.ToString("F0");
            tbxY.Text = _currPos.Y.ToString("F0");

            if (!_isDrawing) return;
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
                if (l == null) return;
                l.X2 = _currPos.X;
                l.Y2 = _currPos.Y;
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
            ReleaseMouseCapture();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPos = e.GetPosition(canvas);
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0 | (Keyboard.Modifiers & ModifierKeys.Alt) != 0)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control ) != 0)
                {
                    _tool = DrawingTool.Ellipsis;
                    _currShape = new Ellipse();
                }
                else
                {
                    _tool = DrawingTool.Rectangle;
                    _currShape = new Rectangle();
                }
                Canvas.SetLeft(_currShape, _startPos.X);
                Canvas.SetTop(_currShape, _startPos.Y);
                _currShape.Width = 2;
                _currShape.Height = 2;
                _currShape.Fill = new SolidColorBrush(_drawColor);
            }
            else
            {
                _tool = DrawingTool.Line;
                var l = new Line
                {
                    Stroke = new SolidColorBrush(_drawColor),
                    StrokeThickness = 2.0,
                    X1 = _startPos.X,
                    Y1 = _currPos.Y,
                    X2 = _currPos.X + 1,
                    Y2 = _currPos.Y + 1
                };
                _currShape = l;
            }
            CaptureMouse();
            canvas.Children.Add(_currShape);
            _isDrawing = true;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.B:
                    SetColor(Colors.Black);
                    break;
                case Key.Y:
                    SetColor(Colors.Yellow);
                    break;
                case Key.G:
                    SetColor(Colors.Green);
                    break;
                case Key.R:
                    SetColor(Colors.Red);
                    break;
                case Key.U:
                    SetColor(Colors.Blue);
                    break;
                default:
                    break;

            }
        }
    }
}
