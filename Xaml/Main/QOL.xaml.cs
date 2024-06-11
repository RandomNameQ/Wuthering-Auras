using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Xaml.Main
{
    /// <summary>
    /// Interaction logic for QOL.xaml
    /// </summary>
    public partial class QOL : Page
    {
        public class HotkeysSettings
        {
            public bool isAutoLoot { get; set; }
            public bool isAutoRuning { get; set; }
            public bool isCanselTarget { get; set; }
            public bool isSwitchTarget { get; set; }
            public string canselTargetHotkey { get; set; }
            public string switchTargetHotkey { get; set; }
        }

        private string filePath = Path.Combine(Helper.jsonPath, $"Hotkeys.json");
        private HotkeysSettings hotkeysSettings;

        public QOL()
        {
            InitializeComponent();

            LoadHotkeysSettings();
        }

        private void SaveHotkeysSettings()
        {
            // Create the hotkeys settings object
            hotkeysSettings = new HotkeysSettings
            {
                isAutoLoot = PressFCheckBox.IsChecked ?? false,
                isAutoRuning = AutoRunCheckBox.IsChecked ?? false,
                isCanselTarget = CanselTargetHotkey.IsChecked ?? false,
                isSwitchTarget = SwitchTargetHotkey.IsChecked ?? false,
                canselTargetHotkey = disableTargetTextBox.Text,
                switchTargetHotkey = lockTargetTextBox.Text
            };

            // Serialize the object to JSON and save to file
            string json = JsonConvert.SerializeObject(hotkeysSettings, Formatting.Indented);
            File.WriteAllText(filePath, json);
            GameStates.Instance.qolHotkey = hotkeysSettings;
        }

        private void LoadHotkeysSettings()
        {
            // Check if the settings file exists
            if (File.Exists(filePath))
            {
                // Load JSON from file and deserialize into hotkeys settings object
                string json = File.ReadAllText(filePath);
                hotkeysSettings = JsonConvert.DeserializeObject<HotkeysSettings>(json);

                // Populate the UI elements with values from the settings object
                PressFCheckBox.IsChecked = hotkeysSettings.isAutoLoot;
                AutoRunCheckBox.IsChecked = hotkeysSettings.isAutoRuning;
                CanselTargetHotkey.IsChecked = hotkeysSettings.isCanselTarget;
                SwitchTargetHotkey.IsChecked = hotkeysSettings.isSwitchTarget;
                disableTargetTextBox.Text = hotkeysSettings.canselTargetHotkey;
                lockTargetTextBox.Text = hotkeysSettings.switchTargetHotkey;
            }
            else
            {
                // If the settings file is not found, create a new hotkeys settings object
                hotkeysSettings = new HotkeysSettings();
            }
            GameStates.Instance.qolHotkey = hotkeysSettings;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save settings when the Save button is clicked
            SaveHotkeysSettings();
        }

        private void Universal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Prevent characters from appearing in the TextBox after pressing a key
                e.Handled = true;

                // Get the pressed key
                Key keyPressed = e.Key;

                // For system keys
                if (keyPressed == Key.System)
                {
                    keyPressed = e.SystemKey;
                }

                // Handle modifier keys if necessary
                if (keyPressed == Key.LeftCtrl || keyPressed == Key.RightCtrl)
                {
                    keyPressed = Key.LeftCtrl; // Or any representation of the Ctrl key you choose
                }

                // Assign the string representation of the key to the TextBox
                textBox.Text = keyPressed.ToString();

                // Here you can save keyPressed or its string representation for further use in your application
            }
        }

        //private void Universal_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (sender is TextBox textBox)
        //    {
        //        e.Handled = true; // To prevent scrolling from affecting other elements

        //        // Check the direction of the scroll
        //        if (e.Delta > 0)
        //        {
        //            textBox.Text = "WU"; // Scroll up
        //        }
        //        else if (e.Delta < 0)
        //        {
        //            textBox.Text = "WD"; // Scroll down
        //        }
        //    }
        //}
    }
}
