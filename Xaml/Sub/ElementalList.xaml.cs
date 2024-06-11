using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wuthering_Waves_comfort_vision.Xaml
{
    /// <summary>
    /// Interaction logic for ElementalList.xaml
    /// </summary>
    public partial class ElementalList : Page
    {
        public event Action<string> ElementalSelected;
        private string _selectedElemental;
        public ElementalList()
        {
            InitializeComponent();
        }


        public void LoadIcons()
        {
            elementalItemsControl.Items.Clear(); // Очищаем текущие элементы, если нужно 

            foreach (var elemental in GameStates.Instance.Elemental)
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
                    Tag = elemental.name // Сохраняем значение в Tag (предполагается, что это строка или int)
                };

                // Добавляем обработчик на кнопку
                button.Click += ElementalButton_Click;

                // Создание и добавление эллипса с изображением в кнопку
                var ellipse = new Ellipse
                {
                    Width = 80,
                    Height = 80,
                    Fill = new ImageBrush(new BitmapImage(new Uri(elemental.skill.path, UriKind.RelativeOrAbsolute)))
                };
                button.Content = ellipse;

                // Добавление кнопки в ItemsControl
                elementalItemsControl.Items.Add(button);
            }
        }

        private void ElementalButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var echo = clickedButton?.Tag as string; // в зависимости от типа данных name в HeroState
            _selectedElemental = echo;
            // выполните здесь действия с помощью данных героя,
            // например, сохраните его выбор или обновите интерфейс
        }

        private void PickElemental_Click(object sender, RoutedEventArgs e)
        {
            // Закрыть эту страницу и передать выбранного героя
            ElementalSelected?.Invoke(_selectedElemental);
            Window.GetWindow(this)?.Close();
        }
    }
}
