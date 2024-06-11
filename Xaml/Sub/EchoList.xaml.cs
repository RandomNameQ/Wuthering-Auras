using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wuthering_Waves_comfort_vision.Xaml
{
    /// <summary>
    /// Interaction logic for EchoList.xaml
    /// </summary>
    public partial class EchoList : Page
    {
        public event Action<string> EchoSelected;
        private string _selectedEcho;
        public EchoList()
        {
            InitializeComponent();
        }


        public void LoadIcons()
        {
            echoItemsControl.Items.Clear(); // Очищаем текущие элементы, если нужно 

            foreach (var echo in GameStates.Instance.Echos)
            {
                // Создание кнопки с изображением героя
                var button = new Button
                {
                    Width = 80,
                    Height = 80,
                    Margin = new Thickness(5),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Background = Brushes.Transparent,
                    Tag = echo.name // Сохраняем значение в Tag (предполагается, что это строка или int)
                };

                // Добавляем обработчик на кнопку
                button.Click += EchoButton_Click;

                // Создание и добавление эллипса с изображением в кнопку
                var ellipse = new Ellipse
                {
                    Width = 80,
                    Height = 80,
                    Fill = new ImageBrush(new BitmapImage(new Uri(echo.skill.path, UriKind.RelativeOrAbsolute)))
                };
                button.Content = ellipse;

                // Добавление кнопки в ItemsControl
                echoItemsControl.Items.Add(button);
            }
        }

        private void EchoButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var echo = clickedButton?.Tag as string; // в зависимости от типа данных name в HeroState
            _selectedEcho = echo;
            // выполните здесь действия с помощью данных героя,
            // например, сохраните его выбор или обновите интерфейс
        }

        private void PickEcho_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть эту страницу и передать выбранного героя
            EchoSelected?.Invoke(_selectedEcho);
            Window.GetWindow(this)?.Close();
        }
    }
}
