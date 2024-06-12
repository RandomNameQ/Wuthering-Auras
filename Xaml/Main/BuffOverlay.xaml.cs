using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;
namespace Wuthering_Waves_comfort_vision.Xaml.Main
{
    public partial class BuffOverlay : Page
    {
        public string heroNumber;
        public Character currentCharacter;
        public Ability currentAbility;
        public BuffOverlay()
        {
            InitializeComponent();
            this.Loaded += LoadPageSettings;
            GlobalEvents.SaveWhenBuffUnrender += SaveUpdatedPositionAfterDestoryClass;
        }

        ~BuffOverlay()
        {
            GlobalEvents.SaveWhenBuffUnrender -= SaveUpdatedPositionAfterDestoryClass;

        }

        private void ProcessHeroAbilities(object hero)
        {
            if (hero == null)
                return;

            FieldInfo[] fields = hero.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(Ability))
                {
                    Ability ability = field.GetValue(hero) as Ability;
                    if (ability != null)
                    {
                        UnrenderImage(ability);
                    }
                }
            }
        }


        private void SaveUpdatedPositionAfterDestoryClass()
        {

            return;
            FieldInfo[] fields = GameStates.Instance.currentTeam.firstHero.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(Ability))
                {
                    Ability ability = field.GetValue(currentCharacter) as Ability;
                    if (ability != null)
                    {
                        RenderImage(ability);
                    }
                }
            }
        }

        #region saveLoad
        private void LoadPageSettings(object sender, RoutedEventArgs e)
        {
            currentCharacter = GameStates.Instance.currentTeam?.firstHero;
            if (currentCharacter == null) return;
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }
            HeroName.Content = GameStates.Instance.currentTeam.firstHero.name;
            var firstHeroEllipse = (Ellipse)FirstHeroButton.Template.FindName("ButtonEllipse", FirstHeroButton);
            var secondHeroEllipse = (Ellipse)SecondHeroButton.Template.FindName("ButtonEllipse", SecondHeroButton);
            var thirdHeroEllipse = (Ellipse)ThirdHeroButton.Template.FindName("ButtonEllipse", ThirdHeroButton);
            firstHeroEllipse.Fill = new ImageBrush(new BitmapImage(new Uri(GameStates.Instance.currentTeam.firstHero.imagePath)));
            secondHeroEllipse.Fill = new ImageBrush(new BitmapImage(new Uri(GameStates.Instance.currentTeam.secondHero.imagePath)));
            thirdHeroEllipse.Fill = new ImageBrush(new BitmapImage(new Uri(GameStates.Instance.currentTeam.thirdHero.imagePath)));
            LoadDataAbility();
        }
        private void LoadDataAbility()
        {
            var selectedItem = AbilityComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string abilityType = selectedItem.Content.ToString();
                FieldInfo fieldInfo = currentCharacter.GetType().GetField(abilityType, BindingFlags.Public | BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    Ability ability = fieldInfo.GetValue(currentCharacter) as Ability;
                    if (ability != null)
                    {
                        currentAbility = ability;
                        LoadFields();
                    }
                }
            }
        }
        private void LoadFields()
        {
            if (currentCharacter == null)
            {
                return;
            }
            NeedActivateCheckBox.IsChecked = currentAbility.isCanRenderImage;
            GlobalBuffCheckBox.IsChecked = currentAbility.isGlobalBuff;
            BuffNextCharacterCheckBox.IsChecked = currentAbility.isBuffForNextCharacter;
            CancelBuffIfSwapCharacterCheckBox.IsChecked = currentAbility.isCancelWhenSwapCharacter;
            ShowNumberCheckBox.IsChecked = currentAbility.isShowCooldown;

            DoubleTapBox.IsChecked = currentAbility.isCanselIfDoubleTap;
            ReuseBuffInSpamBox.IsChecked = currentAbility.isReusedIfSpam;


            DurationTextBox.Text = currentAbility.duration.ToString();
            widthImage.Text = currentAbility.overlayImageData.width.ToString();
            heightImage.Text = currentAbility.overlayImageData.height.ToString();

            HotkeyTextBox.Text = currentAbility.hotKeyToActivate;
            HotkeyDoubleTextBox.Text = currentAbility.hotKeyDoubleTap;


        }
        private void SaveCurrentAbilityData()
        {
            if (currentCharacter == null) return;
            ComboBoxItem selectedItem = AbilityComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;
            string abilityName = selectedItem.Content.ToString();
            FieldInfo fieldInfo = currentCharacter.GetType().GetField(abilityName, BindingFlags.Public | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                Ability ability = fieldInfo.GetValue(currentCharacter) as Ability;
                if (ability != null)
                {
                    ability.isCanRenderImage = NeedActivateCheckBox.IsChecked.GetValueOrDefault();
                    ability.isGlobalBuff = GlobalBuffCheckBox.IsChecked.GetValueOrDefault();
                    ability.isBuffForNextCharacter = BuffNextCharacterCheckBox.IsChecked.GetValueOrDefault();
                    ability.isCancelWhenSwapCharacter = CancelBuffIfSwapCharacterCheckBox.IsChecked.GetValueOrDefault();
                    ability.isShowCooldown = ShowNumberCheckBox.IsChecked.GetValueOrDefault();
                    float.TryParse(DurationTextBox.Text, out ability.duration);
                    int.TryParse(widthImage.Text, out ability.overlayImageData.width);
                    int.TryParse(heightImage.Text, out ability.overlayImageData.height);

                    if (ability.overlayImageData.overlayImage != null)
                    {
                        ability.overlayImageData.x = (int)ability.overlayImageData.overlayImage.Left;
                        ability.overlayImageData.y = (int)ability.overlayImageData.overlayImage.Top;
                    }


                    ability.hotKeyToActivate = HotkeyTextBox.Text;
                    ability.hotKeyDoubleTap = HotkeyDoubleTextBox.Text;

                    ability.isCanselIfDoubleTap = DoubleTapBox.IsChecked.GetValueOrDefault();
                    ability.isReusedIfSpam = ReuseBuffInSpamBox.IsChecked.GetValueOrDefault();



                    //ability.isCanRenderImage = false;

                }
            }
            // Сохраняем данные героя в команду
            GameStates.Instance.currentTeam.SaveAllHero();
        }


        private void Click_SaveAllData(object sender, RoutedEventArgs e)
        {
            SaveCurrentAbilityData();
            GameStates.Instance.currentTeam.SaveAllHero();
            string folderPath = System.IO.Path.Combine(Helper.jsonPath, "Teams");
            string filePath = System.IO.Path.Combine(folderPath, $"{GameStates.Instance.currentTeam.name}.json");
            string json = JsonConvert.SerializeObject(GameStates.Instance.currentTeam, Formatting.Indented);
            File.WriteAllText(filePath, json);
            UserSettings.UpdateTeam(GameStates.Instance.currentTeam);
            LoadDataAbility();

        }
        #endregion





        private void AbilityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentCharacter == null) return;
            LoadDataAbility();
            return;
        }
        private void ChoseHero_Click(object sender, RoutedEventArgs e)
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                switch (button.Tag.ToString())
                {
                    case "FirstHero":
                        currentCharacter = GameStates.Instance.currentTeam.firstHero;
                        break;
                    case "SecondHero":
                        currentCharacter = GameStates.Instance.currentTeam.secondHero;
                        break;
                    case "ThirdHero":
                        currentCharacter = GameStates.Instance.currentTeam.thirdHero;
                        break;
                }
                if (currentCharacter != null)
                {
                    HeroName.Content = currentCharacter.name;
                    LoadDataAbility();
                }
            }
        }







        #region durationText
        private void TextBox_Duration(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && currentAbility != null)
            {
                string input = textBox.Text;
                // Check if the input is a valid float
                if (!IsTextAllowed(input))
                {
                    // Remove invalid characters
                    textBox.Text = RemoveInvalidCharacters(input);
                    // Place the cursor at the end of the text
                    textBox.CaretIndex = textBox.Text.Length;
                }
                // Try parsing the input string to float
                if (float.TryParse(textBox.Text, out float duration))
                {
                    currentAbility.duration = duration;
                }
                else
                {
                    Debug.Write("fail");
                }
            }
        }
        private bool IsTextAllowed(string text)
        {
            // Regex to check if the text is a valid float (supports optional leading sign and decimal point)
            Regex regex = new Regex(@"^-?\d*\.?\d*$");
            return regex.IsMatch(text);
        }
        private string RemoveInvalidCharacters(string text)
        {
            // Remove all characters except digits, decimal point, and leading minus sign
            Regex regex = new Regex(@"[^0-9.-]");
            text = regex.Replace(text, "");
            // Ensure there's only one decimal point
            int firstDecimalIndex = text.IndexOf('.');
            if (firstDecimalIndex >= 0)
            {
                text = text.Substring(0, firstDecimalIndex + 1) + text.Substring(firstDecimalIndex + 1).Replace(".", "");
            }
            // Ensure the minus sign is only at the start
            int firstMinusIndex = text.IndexOf('-');
            if (firstMinusIndex > 0)
            {
                text = text.Replace("-", "");
                text = "-" + text;
            }
            return text;
        }


        #endregion

        #region hotkey
        private void TextBox_Hotkey(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                // Проверяем, нажата ли клавиша
                if (e.Key != Key.None && e.Key != Key.ImeProcessed && e.Key != Key.DeadCharProcessed && e.Key != Key.NoName)
                {
                    // Создаем строковое представление клавиши
                    string keyString = "";
                    // Проверяем наличие модификаторов и добавляем их к строке
                    if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
                        keyString += "Ctrl+";
                    if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)
                        keyString += "Shift+";
                    if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
                        keyString += "Alt+";
                    // Проверяем системные клавиши, чтобы избежать двойного отображения "Alt"
                    if (e.Key == Key.System && (Keyboard.Modifiers & ModifierKeys.Alt) != 0)
                    {
                        // При нажатии Alt+клавиша
                        keyString += e.SystemKey.ToString();
                    }
                    else
                    {
                        // Обработка специальных клавиш и чисел
                        if (e.Key >= Key.D0 && e.Key <= Key.D9)
                        {
                            // Для чисел 0-9
                            keyString += e.SystemKey.ToString();
                        }
                        else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                        {
                            // Для чисел на цифровой клавиатуре
                            keyString += ((char)(e.Key - Key.NumPad0 + '0')).ToString();
                        }
                        else
                        {
                            // Для остальных клавиш
                            keyString += e.Key.ToString();
                        }
                    }
                    // Устанавливаем полученное значение в текстовое поле
                    textBox.Text = keyString;
                    // Помечаем событие как обработанное, чтобы избежать дальнейших обработчиков
                    e.Handled = true;
                }
            }
        }
        private void TextBox_HotkeyDoubleTap(object sender, KeyEventArgs e)
        {
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

                        if (currentAbility?.overlayImageData.overlayImage != null)
                        {
                            currentAbility.overlayImageData.overlayImage.Width = value;
                        }
                    }
                    if (nameElement.Name == "heightImage")
                    {

                        if (currentAbility?.overlayImageData.overlayImage != null)
                        {
                            currentAbility.overlayImageData.overlayImage.Height = value;
                        }
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
        public void UpdateHotkeyText(Ability skill, string hotkey)
        {
            // Удаляем ненужное из строки, оставляем только клавиши
            string cleanedHotkey = hotkey.Replace("System.Windows.Controls.TextBox: ", "");
            // Сохраняем клавиши в объекте Skill
            skill.hotKeyToActivate = cleanedHotkey;
        }
        #endregion







        #region images
        public void Click_UpdateImagePath(object sender, RoutedEventArgs e)
        {
            GameStates.Instance.currentTeam.firstHero.name = GameStates.Instance.currentTeam.firstHero.name;
            LoadDataAbility();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files (*.png)|*.png"; // Указываем фильтр для PNG файлов
            if (openFileDialog.ShowDialog() == true) // В WPF ShowDialog() возвращает bool
            {
                string selectedFilePath = openFileDialog.FileName;
                currentAbility.path = selectedFilePath;
                SaveCurrentAbilityData();
            }
        }
        private void Click_ShowImages(object sender, RoutedEventArgs e)
        {
            FieldInfo[] fields = currentCharacter.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(Ability))
                {
                    Ability ability = field.GetValue(currentCharacter) as Ability;
                    if (ability != null)
                    {
                        RenderImage(ability);
                    }
                }
            }
        }
        private void Click_HideImages(object sender, RoutedEventArgs e)
        {
            FieldInfo[] fields = currentCharacter.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(Ability))
                {
                    Ability ability = field.GetValue(currentCharacter) as Ability;
                    if (ability != null)
                    {
                        UnrenderImage(ability);
                    }
                }
            }
        }
        public void RenderImage(Ability ability)
        {
            if (!ability.isCanRenderImage || ability.isImageNowRender) return;
            ability.isImageNowRender = true;
            var x = ability.overlayImageData.x;
            var y = ability.overlayImageData.y;

            var width = ability.overlayImageData.width;
            var height = ability.overlayImageData.height;
            var path = ability.path;
            OverlayImage imageWindow = Helper.CreateImageWindow(height, width, x, y, true);
            imageWindow.UpdateImage(path, currentAbility);
            imageWindow.cooldownTimerTextBlock.FontSize = width / 2;
            ability.overlayImageData.overlayImage = imageWindow;
        }
        public void UnrenderImage(Ability skill)
        {
            skill.isImageNowRender = false;
            skill.overlayImageData.overlayImage?.Close();
        }
        #endregion









    }
}
