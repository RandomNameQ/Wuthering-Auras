using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Wuthering_Waves_comfort_vision.Scripts.Main;
using Wuthering_Waves_comfort_vision.Scripts.Sub;
using Wuthering_Waves_comfort_vision.Xaml;
using Wuthering_Waves_comfort_vision.Xaml.Sub;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Wuthering_Waves_comfort_vision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly MainWindow_Helper helper;
        public readonly string filePath = Path.Combine(Helper.jsonPath, "AppSettings.json");
        public AppSettings appSettings = new AppSettings();
        public readonly DetectCurrentWindow detectCurrentWindow = new DetectCurrentWindow();
        public readonly OverlayCharacter overlayCharacter = new OverlayCharacter();

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
            CheckboxMain_DetectHotkey.Visibility = Visibility.Collapsed;
            CheckboxMain_DetectHotkey_Wuthering.Visibility = Visibility.Collapsed;
            CheckboxMain_RenderOverlayIfFocus.Visibility = Visibility.Collapsed;
            CheckboxMain_RenderOverlay.Visibility = Visibility.Collapsed;
            CheckboxMain_RenderBuffs.Visibility = Visibility.Collapsed;
            CheckboxMain_SwitchMoveImagePosibility.Visibility = Visibility.Collapsed;
            ButtonAddNewIcon.Visibility = Visibility.Collapsed;

            SaveJson(); // Save data when navigating away from MainWindow

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
            ButtonAddNewIcon.Visibility = Visibility.Visible;

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



        public void LoadAppSettings()
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

        private void Button_AddNewIcon(object sender, RoutedEventArgs e)
        {
            if (UIHelper.ChangeButtonColorOnClick(sender)) return;

            // Создаем новое окно
            var window = new Window
            {
                Width = 900,
                Height = 320,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true,
                ShowInTaskbar = false
            };

            var heroListPage = new HeroList();

            heroListPage.HeroSelected += helper.CreateIconList;
            heroListPage.LoadHeroButtons();
            window.Content = heroListPage;
            window.Show();
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
        MainWindow mainWindow;
        public MainWindow_Helper(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void OnWindowFocusChanged(bool isFocused)
        {
            mainWindow.Dispatcher.Invoke(() =>
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
        public void CreateIconList(string name)
        {
            // Создаем новое окно
            var window = new Window
            {
                Width = 200,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Topmost = true,
                ShowInTaskbar = false
            };

            var heroListPage = new IconList();

            //heroListPage.HeroSelected += helper.CreateIconList;
            //heroListPage.LoadHeroButtons();
            window.Content = heroListPage;
            window.Show();
        }
        #region json
        public void LoadJson()
        {
            try
            {
                // Проверяем, существует ли файл
                if (File.Exists(mainWindow.filePath))
                {
                    // Чтение данных из файла
                    string json = File.ReadAllText(mainWindow.filePath);
                    // Десериализация JSON в объект AppSettings
                    mainWindow.appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                else
                {
                    // Если файл не существует, создаем новый объект AppSettings
                    mainWindow.appSettings = new AppSettings();
                    SaveJson();
                }
                mainWindow.CheckboxMain_DetectHotkey.IsChecked = mainWindow.appSettings.isDetectHotkey;
                mainWindow.CheckboxMain_DetectHotkey_Wuthering.IsChecked = mainWindow.appSettings.isWutheringWindow;
                // CheckboxMain_RenderOverlay.IsChecked = appSettings.isRenderCharacterOverlay;
                mainWindow.CheckboxMain_RenderOverlayIfFocus.IsChecked = mainWindow.appSettings.isRenderIfWutherinfWindow;
                mainWindow.CheckboxMain_SwitchMoveImagePosibility.IsChecked = mainWindow.appSettings.isCanControlPositionSizeIcon;
                GameStates.Instance.appSettings = mainWindow.appSettings;
                GlobalEvents.InvokeSwitchMoveImagePosibility(mainWindow.appSettings.isCanControlPositionSizeIcon);
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
                string json = JsonConvert.SerializeObject(mainWindow.appSettings, Formatting.Indented);
                // Запись JSON в файл
                File.WriteAllText(mainWindow.filePath, json);
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
                if (sender == mainWindow.CheckboxMain_DetectHotkey)
                    mainWindow.appSettings.isDetectHotkey = true;
                else if (sender == mainWindow.CheckboxMain_DetectHotkey_Wuthering)
                    mainWindow.appSettings.isWutheringWindow = true;
                else if (sender == mainWindow.CheckboxMain_RenderOverlayIfFocus)
                    mainWindow.appSettings.isRenderIfWutherinfWindow = true;
                else if (sender == mainWindow.CheckboxMain_RenderOverlay)
                    ToggleRenderCharacterOverlay(true);
                else if (sender == mainWindow.CheckboxMain_RenderBuffs)
                    ToggleRenderBuffsOverlay(true);
                else if (sender == mainWindow.CheckboxMain_SwitchMoveImagePosibility)
                    ToggleControlImagePosibility(true);
                mainWindow.LoadAppSettings();
                SaveJson();
            }
        }
        public void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (EnsureCurrentTeamExists())
            {
                if (sender == mainWindow.CheckboxMain_DetectHotkey)
                    mainWindow.appSettings.isDetectHotkey = false;
                else if (sender == mainWindow.CheckboxMain_DetectHotkey_Wuthering)
                    mainWindow.appSettings.isWutheringWindow = false;
                else if (sender == mainWindow.CheckboxMain_RenderOverlayIfFocus)
                    mainWindow.appSettings.isRenderIfWutherinfWindow = false;
                else if (sender == mainWindow.CheckboxMain_RenderOverlay)
                    ToggleRenderCharacterOverlay(false);
                else if (sender == mainWindow.CheckboxMain_RenderBuffs)
                    ToggleRenderBuffsOverlay(false);
                else if (sender == mainWindow.CheckboxMain_SwitchMoveImagePosibility)
                    ToggleControlImagePosibility(false);
                mainWindow.LoadAppSettings();
                SaveJson();
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
            mainWindow.appSettings.isRenderCharacterOverlay = isEnabled;
            if (isEnabled)
                mainWindow.overlayCharacter.StartRenderOverlay();
            else
                mainWindow.overlayCharacter.StopRenderOverlay();
        }
        private void ToggleRenderBuffsOverlay(bool isEnabled)
        {
            mainWindow.appSettings.isRenderBuffsOverlay = isEnabled;
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
            mainWindow.appSettings.isCanControlPositionSizeIcon = isEnabled;
            GlobalEvents.InvokeSwitchMoveImagePosibility(isEnabled);
        }
        #endregion
    }

}
