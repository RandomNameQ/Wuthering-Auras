using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Wuthering_Waves_comfort_vision.Scripts.Main;
using Wuthering_Waves_comfort_vision.Scripts.Sub;
namespace Wuthering_Waves_comfort_vision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly MainWindow_Helper helper;
        private readonly string filePath = Path.Combine(Helper.jsonPath, "AppSettings.json");
        private readonly AppSettings appSettings = new AppSettings();
        private readonly DetectCurrentWindow detectCurrentWindow = new DetectCurrentWindow();
        private readonly OverlayCharacter overlayCharacter = new OverlayCharacter();

        public MainWindow()
        {
            InitializeComponent();

            helper = new MainWindow_Helper(this);
            ContentFrame.Navigated += ContentFrame_Navigated;
            detectCurrentWindow.WindowFocusChanged += helper.OnWindowFocusChanged;
            Closed += helper.MainWindow_Closed;

            LoadAppSettings();
            LoadJson();
        }


        #region navigate

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            bool isMainWindow = e.Content is MainWindow;

            CheckboxMain_DetectHotkey.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;
            CheckboxMain_DetectHotkey_Wuthering.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;
            CheckboxMain_RenderOverlayIfFocus.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;
            CheckboxMain_RenderOverlay.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;
            CheckboxMain_RenderBuffs.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;
            CheckboxMain_SwitchMoveImagePosibility.Visibility = isMainWindow ? Visibility.Visible : Visibility.Collapsed;

            SaveJson();
        }
        private void Click_MainWindow(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Content = null;
            LoadJson();
        }
        private void TextBlock_Click_SetTeam(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new View.SetTeam());
        }

        private void TextBlock_Click_QOL(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new Wuthering_Waves_comfort_vision.Xaml.Main.QOL());
        }

        private void TextBlock_Click_Skills(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new Wuthering_Waves_comfort_vision.Xaml.Main.SkillLibrary());
        }

        private void Click_TrackingBuffs(object sender, MouseButtonEventArgs e)
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }

            ContentFrame.Navigate(new Wuthering_Waves_comfort_vision.Xaml.Main.BuffOverlay());
        }

        private void Click_OverlayScreenArea(object sender, MouseButtonEventArgs e)
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }

            ContentFrame.Navigate(new Wuthering_Waves_comfort_vision.Xaml.Main.MoveScreenArea());
        }

        #endregion



        private void LoadAppSettings()
        {
            GameStates.Instance.appSettings = appSettings;
        }

        private void LoadJson()
        {
            helper.LoadJson();
        }

        private void SaveJson()
        {
            helper.SaveJson();
        }

        private void SaveBuffPositionWhenUnrender()
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }

            UserSettings.UpdateTeam(GameStates.Instance.currentTeam);

            string folderPath = Path.Combine(Helper.jsonPath, "Teams");
            string filePath = Path.Combine(folderPath, $"{GameStates.Instance.currentTeam.name}.json");
            string json = JsonConvert.SerializeObject(GameStates.Instance.currentTeam, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            helper.Checkbox_Checked(sender, e);
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            helper.Checkbox_Unchecked(sender, e);
        }
    }









    public class AppSettings
    {
        public bool isDetectHotkey { get; set; }
        public bool isWutheringWindow { get; set; }
        public bool isRenderCharacterOverlay { get; set; }
        public bool isRenderIfWutherinfWindow { get; set; }
        public bool isRenderBuffsOverlay { get; set; }
        public bool isCanControlPositionSizeIcon { get; set; }
    }

    public class MainWindow_Helper
    {
        MainWindow MainWindow;
        public MainWindow_Helper(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }
        public void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void OnWindowFocusChanged(bool isFocused)
        {
            MainWindow.Dispatcher.Invoke(() =>
            {
                if (isFocused)
                {
                    GameStates.Instance.isGameWindow = true;
                    //  CheckboxMain_DetectHotkey.Visibility = Visibility.Visible;
                    if (GameStates.Instance.appSettings.isRenderIfWutherinfWindow)
                    {
                        GlobalEvents.InvokeRenderImages();
                    }
                }
                else
                {
                    GameStates.Instance.isGameWindow = false;
                    //  CheckboxMain_DetectHotkey.Visibility = Visibility.Collapsed;
                    if (GameStates.Instance.appSettings.isRenderIfWutherinfWindow)
                    {
                        GlobalEvents.InvokeUnRenderImages();
                    }
                }
            });
        }
        #region json
        public void LoadJson()
        {
            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(MainWindow.filePath))
                {
                    // Чтение данных из файла
                    string json = File.ReadAllText(MainWindow.filePath);
                    // Десериализация JSON в объект AppSettings
                    MainWindow.appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                else
                {
                    // Если файл не существует, создаем новый объект AppSettings
                    MainWindow.appSettings = new AppSettings();
                    SaveJson();
                }
                MainWindow.CheckboxMain_DetectHotkey.IsChecked = MainWindow.appSettings.isDetectHotkey;
                MainWindow.CheckboxMain_DetectHotkey_Wuthering.IsChecked = MainWindow.appSettings.isWutheringWindow;
                // CheckboxMain_RenderOverlay.IsChecked = appSettings.isRenderCharacterOverlay;
                MainWindow.CheckboxMain_RenderOverlayIfFocus.IsChecked = MainWindow.appSettings.isRenderIfWutherinfWindow;
                MainWindow.CheckboxMain_SwitchMoveImagePosibility.IsChecked = MainWindow.appSettings.isCanControlPositionSizeIcon;
                GameStates.Instance.appSettings = MainWindow.appSettings;
                GlobalEvents.InvokeSwitchMoveImagePosibility(MainWindow.appSettings.isCanControlPositionSizeIcon);
            }
            catch (Exception ex)
            {
                // Обработка ошибок чтения файла
                Debug.WriteLine($"Error loading settings: {ex.Message}");
            }
        }
        public void SaveJson()
        {
            try
            {
                // Добавим отладочный вывод перед сохранением
                // Сериализация объекта AppSettings в JSON
                string json = JsonConvert.SerializeObject(MainWindow.appSettings, Formatting.Indented);
                // Запись JSON в файл
                File.WriteAllText(MainWindow.filePath, json);
            }
            catch (Exception ex)
            {
                // Обработка ошибок записи файла
                Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
        #endregion
        #region Checkboxs
        public void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (EnsureCurrentTeamExists())
            {
                if (sender == MainWindow.CheckboxMain_DetectHotkey)
                    MainWindow.appSettings.isDetectHotkey = true;
                else if (sender == MainWindow.CheckboxMain_DetectHotkey_Wuthering)
                    MainWindow.appSettings.isWutheringWindow = true;
                else if (sender == MainWindow.CheckboxMain_RenderOverlayIfFocus)
                    MainWindow.appSettings.isRenderIfWutherinfWindow = true;
                else if (sender == MainWindow.CheckboxMain_RenderOverlay)
                    ToggleRenderCharacterOverlay(true);
                else if (sender == MainWindow.CheckboxMain_RenderBuffs)
                    ToggleRenderBuffsOverlay(true);
                else if (sender == MainWindow.CheckboxMain_SwitchMoveImagePosibility)
                    ToggleControlImagePosibility(true);
                MainWindow.SaveSettings();
            }
        }
        public void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EnsureCurrentTeamExists())
            {
                if (sender == MainWindow.CheckboxMain_DetectHotkey)
                    MainWindow.appSettings.isDetectHotkey = false;
                else if (sender == MainWindow.CheckboxMain_DetectHotkey_Wuthering)
                    MainWindow.appSettings.isWutheringWindow = false;
                else if (sender == MainWindow.CheckboxMain_RenderOverlayIfFocus)
                    MainWindow.appSettings.isRenderIfWutherinfWindow = false;
                else if (sender == MainWindow.CheckboxMain_RenderOverlay)
                    ToggleRenderCharacterOverlay(false);
                else if (sender == MainWindow.CheckboxMain_RenderBuffs)
                    ToggleRenderBuffsOverlay(false);
                else if (sender == MainWindow.CheckboxMain_SwitchMoveImagePosibility)
                    ToggleControlImagePosibility(false);
                MainWindow.SaveSettings();
            }
        }
        private bool EnsureCurrentTeamExists()
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return false;
            }
            return true;
        }
        private void ToggleRenderCharacterOverlay(bool isEnabled)
        {
            MainWindow.appSettings.isRenderCharacterOverlay = isEnabled;
            if (isEnabled)
                MainWindow.overlayCharacter.StartRenderOverlay();
            else
                MainWindow.overlayCharacter.StopRenderOverlay();
        }
        private void ToggleRenderBuffsOverlay(bool isEnabled)
        {
            MainWindow.appSettings.isRenderBuffsOverlay = isEnabled;
            if (isEnabled)
            {
                var app = (App)Application.Current;
                app.hotkeyDetector.HotkeyCollector.Collect();
                app.hotkeyDetector.BuffsHotkey.PreRenderAllImage(true);
            }
            else
            {
                var app = (App)Application.Current;
                app.hotkeyDetector.BuffsHotkey.PreRenderAllImage(false);
            }
        }
        private void ToggleControlImagePosibility(bool isEnabled)
        {
            MainWindow.appSettings.isCanControlPositionSizeIcon = isEnabled;
            GlobalEvents.InvokeSwitchMoveImagePosibility(isEnabled);
        }
        #endregion
    }

}
