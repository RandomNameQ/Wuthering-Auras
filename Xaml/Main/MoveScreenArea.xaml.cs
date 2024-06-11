using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wuthering_Waves_comfort_vision.Data;
namespace Wuthering_Waves_comfort_vision.Xaml.Main
{
    public partial class MoveScreenArea : Page
    {
        string filePath = System.IO.Path.Combine(Helper.jsonPath, $"CharacterOverlay.json");
        OverlayArea firstHero = new();
        OverlayArea secondHero = new();
        OverlayArea thirdHero = new();
        OverlayArea firstUltimate = new();
        OverlayArea secondUltimate = new();
        OverlayArea thirdUltimate = new();
        public OverlayArea currentOverlay = new();
        public OverlayImages.OverlayImage originalArea;
        public OverlayImages.OverlayImage overlayArea;
        public List<OverlayImages.OverlayImage> createImages = new();


        public MoveScreenArea()
        {
            InitializeComponent();
            this.Loaded += LoadPageSettings;
        }
        private void LoadPageSettings(object sender, RoutedEventArgs e)
        {
            LoadJson();
            currentOverlay = firstHero;
            LoadDataFromJsonToInterface();



            //firstHero.overlayArea.overlayImage.Height = firstHero.overlayArea.height;
            //firstHero.overlayArea.overlayImage.Width = firstHero.overlayArea.width;
        }
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = Combobox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string tag = selectedItem.Tag as string;
                switch (tag)
                {
                    case "first":
                        currentOverlay = firstHero;

                        break;
                    case "second":
                        currentOverlay = secondHero;

                        break;
                    case "third":
                        currentOverlay = thirdHero;

                        break;
                    case "firstUltimate":
                        currentOverlay = firstUltimate;
                        currentOverlay.currentCharacter = GameStates.Instance.currentTeam.firstHero;
                        currentOverlay.isStopUpdateIfNewCharacter = true;
                        break;
                    case "secondUltimate":
                        currentOverlay = secondUltimate;
                        currentOverlay.currentCharacter = GameStates.Instance.currentTeam.secondHero;
                        currentOverlay.isStopUpdateIfNewCharacter = true;
                        break;
                    case "thirdUltimate":
                        currentOverlay = thirdUltimate;
                        currentOverlay.currentCharacter = GameStates.Instance.currentTeam.thirdHero;
                        currentOverlay.isStopUpdateIfNewCharacter = true;
                        break;
                    default:
                        Debug.Write("fail");
                        break;
                }
                LoadDataFromJsonToInterface();
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            currentOverlay.isOverlayEnable = true;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            currentOverlay.isOverlayEnable = false;
        }
        private void Button_OriginalArea(object sender, System.Windows.RoutedEventArgs e)
        {
            if (currentOverlay.originalArea.isCreated)
            {
                return;
            }
            var height = currentOverlay.originalArea.height;
            var width = currentOverlay.originalArea.width;
            var x = currentOverlay.originalArea.x;
            var y = currentOverlay.originalArea.y;
            originalArea = Helper.CreateImageWindow(height, width, x, y);
            originalArea.cooldownTimerTextBlock.Text = currentOverlay.number.ToString();
            originalArea.border.BorderBrush = System.Windows.Media.Brushes.Red;
            currentOverlay.originalArea.isCreated = true;
            createImages.Add(originalArea);
        }
        private void Button_OverlayArea(object sender, System.Windows.RoutedEventArgs e)
        {
            if (currentOverlay.overlayArea.isCreated)
            {
                return;
            }
            var height = currentOverlay.overlayArea.height;
            var width = currentOverlay.overlayArea.width;
            var x = currentOverlay.overlayArea.x;
            var y = currentOverlay.overlayArea.y;


            overlayArea = Helper.CreateImageWindow(height, width, x, y);

            overlayArea.cooldownTimerTextBlock.Text = currentOverlay.number.ToString();
            overlayArea.border.BorderBrush = System.Windows.Media.Brushes.Blue;
            currentOverlay.overlayArea.isCreated = true;
            createImages.Add(overlayArea);
        }
        private void Button_Hide(object sender, RoutedEventArgs e)
        {
            foreach (var image in createImages)
            {
                image.Close();
            }
            createImages.Clear();
            firstHero.overlayArea.isCreated = false;
            firstHero.originalArea.isCreated = false;
            secondHero.overlayArea.isCreated = false;
            secondHero.originalArea.isCreated = false;
            thirdHero.overlayArea.isCreated = false;
            thirdHero.originalArea.isCreated = false;
            firstUltimate.overlayArea.isCreated = false;
            firstUltimate.originalArea.isCreated = false;
            secondUltimate.overlayArea.isCreated = false;
            secondUltimate.originalArea.isCreated = false;
            thirdUltimate.overlayArea.isCreated = false;
            thirdUltimate.originalArea.isCreated = false;
        }
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (originalArea != null)
            {
                currentOverlay.originalArea.x = (int)originalArea.Left;
                currentOverlay.originalArea.y = (int)originalArea.Top;
                currentOverlay.originalArea.width = (int)originalArea.Width;
                currentOverlay.originalArea.height = (int)originalArea.Height;
            }
            if (overlayArea != null)
            {
                currentOverlay.overlayArea.x = (int)overlayArea.Left;
                currentOverlay.overlayArea.y = (int)overlayArea.Top;
                currentOverlay.overlayArea.width = (int)overlayArea.Width;
                currentOverlay.overlayArea.height = (int)overlayArea.Height;
            }
            SaveJson();
        }
        public void SaveJson()
        {
            var settings = new
            {
                FirstHero = firstHero,
                SecondHero = secondHero,
                ThirdHero = thirdHero,
                UltimateArea1 = firstUltimate,
                UltimateArea2 = secondUltimate,
                UltimateArea3 = thirdUltimate
            };
            settings.FirstHero.number = 1;
            settings.SecondHero.number = 2;
            settings.ThirdHero.number = 3;
            settings.UltimateArea1.number = 10;
            settings.UltimateArea2.number = 20;
            settings.UltimateArea3.number = 30;
            string jsonData = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }
        public void LoadJson()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<dynamic>(jsonData);
                firstHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.FirstHero));
                secondHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.SecondHero));
                thirdHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.ThirdHero));
                firstUltimate = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea1));
                secondUltimate = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea2));
                thirdUltimate = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea3));

            }
            else
            {
                // Создание нового объекта с начальными значениями и сохранение его в файл
                firstHero = new OverlayArea { number = 1 };
                secondHero = new OverlayArea { number = 2 };
                thirdHero = new OverlayArea { number = 3 };
                firstUltimate = new OverlayArea { number = 10 };
                secondUltimate = new OverlayArea { number = 20 };
                thirdUltimate = new OverlayArea { number = 30 };
                SaveJson();
            }
        }
        public void LoadDataFromJsonToInterface()
        {
            if (EnableOverlay != null)
            {
                EnableOverlay.IsChecked = currentOverlay.isOverlayEnable;
                widthImage.Text = currentOverlay.overlayArea.width.ToString();
                heightImage.Text = currentOverlay.overlayArea.height.ToString();
            }
        }


        private void TextBox_WidthHeight(object sender, TextChangedEventArgs e)
        {
            var nameElement = sender as TextBox;
            if (nameElement != null)
            {
                int value;
                if (int.TryParse(nameElement.Text, out value))
                {
                    if (nameElement.Name == "widthImage")
                    {
                        currentOverlay.overlayArea.width = value;
                    }
                    else if (nameElement.Name == "heightImage")
                    {
                        currentOverlay.overlayArea.height = value;
                    }
                }
            }
        }




        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedJustNumber(e.Text);
        }
        private static bool IsTextAllowedJustNumber(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
