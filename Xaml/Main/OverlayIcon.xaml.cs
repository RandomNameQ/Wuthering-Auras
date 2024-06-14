using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Wuthering_Waves_comfort_vision.Xaml.Main
{
    /// <summary>
    /// Interaction logic for OverlayIcon.xaml
    /// </summary>
    public partial class OverlayIcon : Window
    {
        private const int SnapDistance = 20; // Distance for snapping

        public OverlayIcon()
        {
            InitializeComponent();
            this.SizeChanged += OverlayIcon_SizeChanged;
        }

        // Ensures width and height are equal when resized
        private void OverlayIcon_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newSize = Math.Max(this.Width, this.Height);
            this.Width = newSize;
            this.Height = newSize;
        }

        public void Subscribe()
        {
            this.MouseDown += OverlayImage_MouseDown;
            this.MouseMove += OverlayImage_MouseMove;
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        public void UnSubscribe()
        {
            this.MouseDown -= OverlayImage_MouseDown;
            this.MouseMove -= OverlayImage_MouseMove;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void OverlayImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //if (Height > Width)
                //{
                //    Width = Height;
                //}
                //else
                //{
                //    Height = Width;
                //}
                this.DragMove();
            }
        }

        private void OverlayImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
                SnapToEdges();
            }
        }

        // Snaps the window to the edges of the screen if close enough
        private void SnapToEdges()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            var left = this.Left;
            var top = this.Top;
            var right = left + this.Width;
            var bottom = top + this.Height;

            if (Math.Abs(left) < SnapDistance)
                this.Left = 0;

            if (Math.Abs(top) < SnapDistance)
                this.Top = 0;

            if (Math.Abs(screenWidth - right) < SnapDistance)
                this.Left = screenWidth - this.Width;

            if (Math.Abs(screenHeight - bottom) < SnapDistance)
                this.Top = screenHeight - this.Height;
        }

        public class Data
        {
            public int x, y;
            public int width, height;
            public Color borderColor;
        }
    }
}
