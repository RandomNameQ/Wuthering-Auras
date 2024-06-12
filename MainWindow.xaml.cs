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
        private string filePath = System.IO.Path.Combine(Helper.jsonPath, "AppSettings.json");
        private AppSettings appSettings = new();
        private DetectCurrentWindow detectCurrentWindow;
        private OverlayCharacter overlayCharacter;
        public MainWindow()
        {
            InitializeComponent();
            LoadJson();
            GameStates.Instance.appSettings.isRenderCharacterOverlay = false;
            overlayCharacter = new();
            ContentFrame.Navigated += ContentFrame_Navigated;
            detectCurrentWindow = new DetectCurrentWindow();
            detectCurrentWindow.WindowFocusChanged += OnWindowFocusChanged;
            this.Closed += MainWindow_Closed;
        }
        public class AppSettings
        {
            public bool isDetectHotkey;
            public bool isWutheringWindow;
            public bool isRenderCharacterOverlay;
            public bool isRenderIfWutherinfWindow;
            public bool isRenderBuffsOverlay;
            public bool isCanControlPositionSizeIcon;
        }
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is MainWindow))
            {
                // Выполнить код только если не переходим на MainWindow
                CheckboxMain_DetectHotkey.Visibility = Visibility.Collapsed;
                CheckboxMain_DetectHotkey_Wuthering.Visibility = Visibility.Collapsed;
                CheckboxMain_RenderOverlayIfFocus.Visibility = Visibility.Collapsed;

                CheckboxMain_RenderOverlay.Visibility = Visibility.Collapsed;
                CheckboxMain_RenderBuffs.Visibility = Visibility.Collapsed;
                CheckboxMain_SwitchMoveImagePosibility.Visibility = Visibility.Collapsed;
                SaveJson();
            }
        }
        private void Click_MainWindow(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Content = null;
            CheckboxMain_DetectHotkey.Visibility = Visibility.Visible;
            CheckboxMain_DetectHotkey_Wuthering.Visibility = Visibility.Visible;
            CheckboxMain_RenderOverlayIfFocus.Visibility = Visibility.Visible;

            CheckboxMain_RenderOverlay.Visibility = Visibility.Visible;
            CheckboxMain_RenderBuffs.Visibility = Visibility.Visible;
            CheckboxMain_SwitchMoveImagePosibility.Visibility = Visibility.Visible;
            LoadJson();
        }
        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }
            if (sender == CheckboxMain_DetectHotkey)
            {
                appSettings.isDetectHotkey = true;
            }
            else if (sender == CheckboxMain_DetectHotkey_Wuthering)
            {
                appSettings.isWutheringWindow = true;
            }

            else if (sender == CheckboxMain_RenderOverlayIfFocus)
            {
                appSettings.isRenderIfWutherinfWindow = true;
            }



            else if (sender == CheckboxMain_RenderOverlay)
            {
                appSettings.isRenderCharacterOverlay = true;
                if (appSettings.isRenderCharacterOverlay)
                {
                    overlayCharacter.StartRenderOverlay();
                }
            }
            else if (sender == CheckboxMain_RenderBuffs)
            {
                appSettings.isRenderBuffsOverlay = true;
                if (GameStates.Instance.appSettings.isRenderBuffsOverlay)
                {
                    var app = (App)Application.Current;
                    app.hotkeyDetector.HotkeyCollector.Collect();
                    app.hotkeyDetector.BuffsHotkey.PreRenderAllImage(true);
                }
            }

            else if (sender == CheckboxMain_SwitchMoveImagePosibility)
            {
                appSettings.isCanControlPositionSizeIcon = true;
                GlobalEvents.InvokeSwitchMoveImagePosibility(true);
            }


            GameStates.Instance.appSettings = appSettings;
            SaveJson();
        }
        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (GameStates.Instance.currentTeam == null)
            {
                MessageBox.Show("Team not created");
                return;
            }
            if (sender == CheckboxMain_DetectHotkey)
            {
                appSettings.isDetectHotkey = false;
            }
            else if (sender == CheckboxMain_DetectHotkey_Wuthering)
            {
                appSettings.isWutheringWindow = false;
            }

            else if (sender == CheckboxMain_RenderOverlayIfFocus)
            {
                appSettings.isRenderIfWutherinfWindow = false;
            }

            else if (sender == CheckboxMain_RenderOverlay)
            {
                appSettings.isRenderCharacterOverlay = false;
                overlayCharacter.StopRenderOverlay();
            }

            else if (sender == CheckboxMain_RenderBuffs)
            {

                appSettings.isRenderBuffsOverlay = false;

                var app = (App)Application.Current;
                app.hotkeyDetector.BuffsHotkey.PreRenderAllImage(false);
                //GlobalEvents.InvokeSaveBuffDataWhenUnRender();

                SaveBuffPositionWhenUnrender();


            }

            else if (sender == CheckboxMain_SwitchMoveImagePosibility)
            {
                appSettings.isCanControlPositionSizeIcon = false;
                GlobalEvents.InvokeSwitchMoveImagePosibility(false);

            }
            GameStates.Instance.appSettings = appSettings;
            SaveJson();
        }

        private void SaveBuffPositionWhenUnrender()
        {
            UserSettings.UpdateTeam(GameStates.Instance.currentTeam);

            string folderPath = System.IO.Path.Combine(Helper.jsonPath, "Teams");
            string filePath = System.IO.Path.Combine(folderPath, $"{GameStates.Instance.currentTeam.name}.json");
            string json = JsonConvert.SerializeObject(GameStates.Instance.currentTeam, Formatting.Indented);
            File.WriteAllText(filePath, json);

        }

        private void LoadJson()
        {
            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    // Чтение данных из файла
                    string json = File.ReadAllText(filePath);
                    // Десериализация JSON в объект AppSettings
                    appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                else
                {
                    // Если файл не существует, создаем новый объект AppSettings
                    appSettings = new AppSettings();
                    SaveJson();
                }
                CheckboxMain_DetectHotkey.IsChecked = appSettings.isDetectHotkey;
                CheckboxMain_DetectHotkey_Wuthering.IsChecked = appSettings.isWutheringWindow;
                // CheckboxMain_RenderOverlay.IsChecked = appSettings.isRenderCharacterOverlay;
                CheckboxMain_RenderOverlayIfFocus.IsChecked = appSettings.isRenderIfWutherinfWindow;
                CheckboxMain_SwitchMoveImagePosibility.IsChecked = appSettings.isCanControlPositionSizeIcon;
                GameStates.Instance.appSettings = appSettings;
                GlobalEvents.InvokeSwitchMoveImagePosibility(appSettings.isCanControlPositionSizeIcon);

            }
            catch (Exception ex)
            {
                // Обработка ошибок чтения файла
                Debug.WriteLine($"Error loading settings: {ex.Message}");
            }
        }
        private void SaveJson()
        {
            try
            {
                // Добавим отладочный вывод перед сохранением
                // Сериализация объекта AppSettings в JSON
                string json = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
                // Запись JSON в файл
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // Обработка ошибок записи файла
                Debug.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
        private void TextBlock_Click_SetTeam(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new View.SetTeam());
        }
        private void TextBlock_Click_QOL(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new Wuthering_Waves_comfort_vision.Xaml.Main.QOL());
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
        private void OnWindowFocusChanged(bool isFocused)
        {
            Dispatcher.Invoke(() =>
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
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // Завершение приложения при закрытии главного окна
            Application.Current.Shutdown();
        }
    }
}
