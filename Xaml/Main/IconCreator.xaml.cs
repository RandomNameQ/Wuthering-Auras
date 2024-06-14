using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Wuthering_Waves_comfort_vision.Data.Skill;
using Wuthering_Waves_comfort_vision.Xaml.Main;

namespace Wuthering_Waves_comfort_vision.Xaml.Sub
{
    /// <summary>
    /// Interaction logic for IconCreator.xaml
    /// </summary>
    public partial class IconCreator : Page, INotifyPropertyChanged
    {
        SkillLibrary_Heller helper;
        private ObservableCollection<Skill> _skills = new();
        private Skill _skill;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        public Skill Skill
        {
            get => _skill;
            set
            {
                _skill = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IconCreator()
        {
            helper = new(this);
            _skill = new Skill();

            InitializeComponent();
            Loaded += SkillLibrary_Loaded; // Подписываемся на событие Loaded
        }


        private void SkillLibrary_Loaded(object sender, RoutedEventArgs e)
        {
            _skills = helper.LoadJson();
            FillingCombobox();
        }

        private void FillingCombobox()
        {
            TypeSkill.ItemsSource = Enum.GetNames(typeof(Skill.SkillType)).ToList();
            ActionTypeSkill.ItemsSource = Enum.GetNames(typeof(Skill.SkillActionType)).ToList();
            CharacterVariant.ItemsSource = Enum.GetNames(typeof(Skill.CharacterVariant)).ToList();

            ComboBox_Skill.ItemsSource = _skills.Select(s => s.name).ToList();
        }

        #region leftUI
        private void Button_AddNewSkill(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (UIHelper.ChangeButtonColorOnClick(sender)) return;

            //_skill = new Skill();
            //helper.ResetData();

            //_skill.name = helper.GetUnicName(_skill.name, _skills);

            //var tempSkills = helper.GetValueForComobox(_skills, _skill);

            //ComboBox_Skill.ItemsSource = tempSkills.Select(s => s.name).ToList();
            //ComboBox_Skill.SelectedIndex = ComboBox_Skill.Items.Count - 1;
        }


        private void ComboBox_Skill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Skill.SelectedItem != null)
            {
                DataContext = this;
                string selectedSkillName = ComboBox_Skill.SelectedItem.ToString();
                _skill = helper.LoadData(_skills, selectedSkillName);

                OnPropertyChanged(nameof(Skill));
            }
        }



        private void Button_DeleteSkill(object sender, RoutedEventArgs e)
        {
            // Отображаем всплывающее окно с подтверждением удаления
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete '{_skill.name}' skill?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Проверяем результат нажатия кнопки
            if (result == MessageBoxResult.Yes)
            {
                // Удаляем навык
                DeleteSkill();
            }
        }

        private void DeleteSkill()
        {
            // Ваш код для удаления навыка
        }
        #endregion

        #region midUI

        private void NameSkill_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_skill.name = UIHelper.GetString(sender);
        }
        private void DescriptionSkill_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_skill.description = UIHelper.GetString(sender);
        }


        private void ComboBox_SkillType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Skill == null) Skill = new();

            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedString)
            {
                if (Enum.TryParse(typeof(Skill.SkillType), selectedString, out object result))
                {
                    Skill.skillType = (Skill.SkillType)result;
                }
            }
        }

        private void ComboBox_SkillActionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Skill == null) Skill = new();

            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedString)
            {
                if (Enum.TryParse(typeof(Skill.SkillActionType), selectedString, out object result))
                {
                    Skill.skillActionType = (Skill.SkillActionType)result;
                }
            }
        }


        private void ComboBox_CharacterVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Skill == null) Skill = new();

            if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedString)
            {
                if (Enum.TryParse(typeof(Skill.CharacterVariant), selectedString, out object result))
                {
                    Skill.characterVariant = (Skill.CharacterVariant)result;
                }
            }
        }


        private void DurationSkill_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            string text = UIHelper.AllowOnlyNumbers(textBox.Text);
            textBox.Text = text;
            textBox.CaretIndex = text.Length;

            if (double.TryParse(text, out double result))
            {
                _skill.duration = result;
            }
            else
            {
                // В случае некорректного ввода, можно установить значение по умолчанию или предпринять другие действия
                _skill.duration = 0; // Например, установка в ноль
            }
        }

        // Вспомогательный метод в UIHelper для разрешения только цифр



        private void Button_ImagePath(object sender, System.Windows.RoutedEventArgs e)
        {
            UIHelper.GetPath();
        }

        #endregion








        #region rightUI
        private void Button_CreateIcon(object sender, System.Windows.RoutedEventArgs e)
        {
            if (UIHelper.ChangeButtonColorOnClick(sender)) return;

            //UpdateTextInSkill();
            //_skills.Add(_skill);
            //helper.SaveJson(_skills);

            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            var overlayWidth = 50; // or overlayImage.Width
            var overlayHeight = 50; // or overlayImage.Height

            var centerX = (screenWidth / 2) - (overlayWidth / 2);
            var centerY = (screenHeight / 2) - (overlayHeight / 2);

            var overlayImage = new OverlayIcon
            {
                Width = 50,
                Height = 50,
                Left = centerX,
                Top = centerY,
                WindowStartupLocation = WindowStartupLocation.Manual,
                WindowStyle = WindowStyle.None,

                AllowsTransparency = true,
                Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 255, 255)),
                Topmost = true,
                ShowInTaskbar = false

            };



            overlayImage.Subscribe();

            overlayImage.Show();

            // MessageBox.Show($"Skill '{skill.name}' is saved");

            //ComboBox_Skill.ItemsSource = _skills.Select(s => s.name).ToList();
            //ComboBox_Skill.SelectedIndex = ComboBox_Skill.Items.Count - 1;



        }

        private void UpdateTextInSkill()
        {
            _skill.description = DescriptionSkill.Text;
            _skill.name = NameSkill.Text;

            if (double.TryParse(DurationSkill.Text, out double duration))
            {
                _skill.duration = duration;
            }
            else
            {
                // Обработка случая, когда введенное значение не может быть преобразовано в double
                // Например, можно вывести сообщение об ошибке или установить значение по умолчанию
                MessageBox.Show("Duration field is corrupted");
                // Пример установки значения по умолчанию
                _skill.duration = 0.0; // или другое значение по вашему усмотрению
            }

        }
        #endregion



    }

    public class SkillLibrary_Heller
    {
        string filePath = System.IO.Path.Combine(Helper.jsonPath + "/Skills");

        IconCreator skillLibrary;
        Skill skill;
        public SkillLibrary_Heller(IconCreator skillLibrary)
        {
            this.skillLibrary = skillLibrary;
        }

        public void ResetData()
        {
            skillLibrary.TypeSkill.SelectedIndex = 0;
            skillLibrary.ActionTypeSkill.SelectedIndex = 0;
        }

        public Skill LoadData(ObservableCollection<Skill> skills, string name)
        {
            Skill skill = skills.FirstOrDefault(s => s.name == name);
            if (skill != null)
            {
                skillLibrary.DescriptionSkill.Text = skill.description;
                skillLibrary.NameSkill.Text = skill.name;
                skillLibrary.DurationSkill.Text = skill.duration.ToString();
                skillLibrary.ActionTypeSkill.SelectedItem = skill.skillActionType.ToString();
                skillLibrary.TypeSkill.SelectedItem = skill.skillType.ToString();
            }
            return skill;
        }

        #region json
        public ObservableCollection<Skill> LoadJson()
        {
            ObservableCollection<Skill> loadedSkills = new ObservableCollection<Skill>();
            string directoryPath = Path.Combine(Helper.jsonPath, "Skills");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var file in Directory.GetFiles(directoryPath, "*.json"))
            {
                string json = File.ReadAllText(file);
                Skill skill = JsonConvert.DeserializeObject<Skill>(json, new StringEnumConverter());
                loadedSkills.Add(skill);
            }

            return loadedSkills;
        }

        public void SaveJson(ObservableCollection<Skill> skills)
        {
            string directoryPath = Path.Combine(Helper.jsonPath, "Skills");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var skill in skills)
            {
                string json = JsonConvert.SerializeObject(skill, Formatting.Indented, new StringEnumConverter());
                string filePath = Path.Combine(directoryPath, skill.name + ".json");
                File.WriteAllText(filePath, json);
            }
        }

        #endregion
        public bool IsSameName(string sourceName, ObservableCollection<Skill> skills)
        {
            foreach (var skill in skills)
            {
                if (skill.name == sourceName)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetUnicName(string sourceName, ObservableCollection<Skill> skills)
        {
            string uniqueName = sourceName;
            Random random = new Random();

            while (IsSameName(uniqueName, skills))
            {
                string randomDigits = random.Next(10000, 100000).ToString(); // Generate 5 random digits
                uniqueName = sourceName + "_" + randomDigits;
            }

            return uniqueName;
        }


        public ObservableCollection<Skill> GetValueForComobox(ObservableCollection<Skill> skills, Skill skill)
        {
            ObservableCollection<Skill> tempSkills = new ObservableCollection<Skill>(skills);
            tempSkills.Add(skill);

            return tempSkills;
        }


    }
}
