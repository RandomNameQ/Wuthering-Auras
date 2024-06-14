using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wuthering_Waves_comfort_vision.Xaml
{
    public partial class HeroList : Page
    {
        public event Action<string> HeroSelected;
        private string _selectedHero;
        public HeroList()
        {
            InitializeComponent();
        }


        public void LoadHeroButtons()
        {
            heroItemsControl.Items.Clear(); // Очищаем текущие элементы, если нужно 

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
                button.Click += HeroButton_Click;

                // Создание и добавление эллипса с изображением в кнопку
                var ellipse = new Ellipse
                {
                    Width = 80,
                    Height = 80,
                    Fill = new ImageBrush(new BitmapImage(new Uri(hero.imagePath, UriKind.RelativeOrAbsolute)))
                };
                button.Content = ellipse;

                // Добавление кнопки в ItemsControl
                heroItemsControl.Items.Add(button);
            }
        }



        private void HeroButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            var heroName = clickedButton?.Tag as string;
            _selectedHero = heroName;

        }

        private void PickHero_Click(object sender, RoutedEventArgs e)
        {
            HeroSelected?.Invoke(_selectedHero);
            Window.GetWindow(this)?.Close();
        }




    }
}



