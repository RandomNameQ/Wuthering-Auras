using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wuthering_Waves_comfort_vision.Xaml.Sub
{
    /// <summary>
    /// Interaction logic for IconList.xaml
    /// </summary>
    public partial class IconList : Page
    {
        public IconList()
        {
            InitializeComponent();
        }


        private void Button_AddIcon(object sender, RoutedEventArgs e)
        {
            if (UIHelper.ChangeButtonColorOnClick(sender)) return;
            // Создаем новое окно
            var window = new Window
            {
                Width = 700,
                Height = 470,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true,
                ShowInTaskbar = false
            };

            var heroListPage = new IconCreator();

            //heroListPage.HeroSelected += helper.CreateIconList;
            //heroListPage.LoadHeroButtons();
            window.Content = heroListPage;
            window.Show();

        }
        public void AddIcons()
        {
            // heroItemsControl.Items.Clear(); // Очищаем текущие элементы, если нужно 

            foreach (var hero in GameStates.Instance.Characters)
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
                    Tag = hero.name // Сохраняем значение в Tag (предполагается, что это строка или int)
                };

                // Добавляем обработчик на кнопку 
                button.Click += Button_AddIcon;

                // Создание и добавление эллипса с изображением в кнопку
                var ellipse = new Ellipse
                {
                    Width = 80,
                    Height = 80,
                    Fill = new ImageBrush(new BitmapImage(new Uri(hero.imagePath, UriKind.RelativeOrAbsolute)))
                };
                button.Content = ellipse;

                // Добавление кнопки в ItemsControl
                // heroItemsControl.Items.Add(button);
            }


        }

        private void Button_ChooseIcon(object sender, RoutedEventArgs e)
        {
            // Reset all buttons to default background
            foreach (var child in ButtonStackPanel.Children)
            {
                if (child is Button button)
                {
                    button.ClearValue(Button.BackgroundProperty);
                }
            }

            // Set the clicked button's background
            var clickedButton = sender as Button;
            if (clickedButton != null)
            {
                clickedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF8F5975"));
            }
        }


    }
}
