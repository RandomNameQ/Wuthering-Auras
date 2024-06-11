using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages
{
    public partial class OverlayImage : System.Windows.Window
    {
        public Ability ability;
        public OverlayImage()
        {
            InitializeComponent();
        }
        public string ImagePath
        {
            get
            {
                // Получить источник изображения
                var source = imageDisplay.Source as BitmapImage;
                if (source != null)
                {
                    // Получить UriSource
                    var uriSource = source.UriSource;
                    if (uriSource != null)
                    {
                        // Преобразовать UriSource в строку и вернуть ее
                        return uriSource.ToString();
                    }
                }
                return null; // Если изображение не установлено или UriSource не определен
            }
        }
        public void UpdateImage(BitmapSource newImage)
        {
            if (newImage != null)
            {
                imageDisplay.Source = newImage;
            }
        }
        public void UpdateImage(string path, Ability ability)
        {
            this.ability = ability;
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var uri = new Uri(path, UriKind.RelativeOrAbsolute);
                    var bitmapImage = new BitmapImage(uri);
                    imageDisplay.Source = bitmapImage;
                    if (ability.isGlobalBuff)
                    {
                        border.BorderBrush = System.Windows.Media.Brushes.Red;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image from path: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Path is null or empty");
            }
        }
        public void OverlayImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
                DebufState();
            }
        }
        public void OverlayImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        public void SaveDataAfterChangedPositionOrSize(object sender, MouseButtonEventArgs e)
        {
            // Логика для сохранения данных после изменения позиции или размера
            var window = sender as System.Windows.Window;
            if (window != null)
            {
                Debug.WriteLine($"Window position: {window.Left}, {window.Top}");
                Debug.WriteLine($"Window size: {window.Width} x {window.Height}");
            }
        }

        private void DebufState()
        {
            Debug.WriteLine($"Window position: {this.Left}, {this.Top}");
            Debug.WriteLine($"Window size: {this.Width} x {this.Height}");

        }
        public void CloseWindow()
        {
            this.Close();
        }
        public void CreateIcon()
        {
        }

        public void Subscribe()
        {
            this.MouseDown += OverlayImage_MouseDown;
            this.MouseMove += OverlayImage_MouseMove;
            this.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
        }
        public void UnSubscribe()
        {
            this.MouseDown -= OverlayImage_MouseDown;
            this.MouseMove -= OverlayImage_MouseMove;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
        }
    }
}
