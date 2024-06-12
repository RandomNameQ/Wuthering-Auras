using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wuthering_Waves_comfort_vision.Data.Team;
using Wuthering_Waves_comfort_vision.Xaml;
namespace Wuthering_Waves_comfort_vision.View
{
    public partial class SetTeam : Page
    {
        private Button _selectedButton;
        private List<Team> teams = new List<Team>();
        public Team currentTeam = new();
        public SetTeam()
        {
            InitializeComponent();
            this.Loaded += SetHeroes_Loaded;
        }
        private void SetHeroes_Loaded(object sender, RoutedEventArgs e)
        {
            currentTeam = UserSettings.GetTeam();
            if (currentTeam == null)
            {
                currentTeam = new();
                return;
            }
            LoadJsonTeams();
            PopulateComboBox();
            UpdateTeamInterface();
        }
        public void TryLoadAnyTeam()
        {
        }
        public void LoadList()
        {
            if (currentTeam == null)
            {
                //MessageBox.Show("Saved team not found");
                return;
            }
            LoadJsonTeams();
            PopulateComboBox();
            UpdateTeamInterface();
        }
        private void Hero_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectionWindow(sender, new HeroList(), OnHeroSelected);
        }
        private void OnHeroSelected(string selectedHero)
        {
            UpdateSelection(selectedHero, "hero");
        }
        private void Echo_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectionWindow(sender, new EchoList(), OnEchoSelected);
        }
        private void OnEchoSelected(string selectedEcho)
        {
            UpdateSelection(selectedEcho, "echo");
        }
        private void Elemental_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectionWindow(sender, new ElementalList(), OnElementalSelected);
        }
        private void OnElementalSelected(string selectedElemental)
        {
            UpdateSelection(selectedElemental, "elemental");
        }
        private void OpenSelectionWindow(object sender, Page selectionPage, Action<string> selectionCallback)
        {
            _selectedButton = sender as Button;
            if (_selectedButton == null) return;
            // Subscribe to the selection event
            if (selectionPage is HeroList heroList)
            {
                heroList.HeroSelected += selectionCallback;
                heroList.LoadHeroButtons();
            }
            else if (selectionPage is EchoList echoList)
            {
                echoList.EchoSelected += selectionCallback;
                echoList.LoadIcons();
            }
            else if (selectionPage is ElementalList elementalList)
            {
                elementalList.ElementalSelected += selectionCallback;
                elementalList.LoadIcons();
            }
            // Create a window to host the selection page
            var window = new Window
            {
                Content = selectionPage,
                Title = $"{selectionPage.GetType().Name} List",
                Height = 320,
                Width = 900,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.ShowDialog();
        }
        private void UpdateSelection(string selectedName, string type)
        {
            if (_selectedButton == null || string.IsNullOrEmpty(selectedName)) return;
            string imagePath = Helper.GetPathImage(selectedName, type);
            if (string.IsNullOrEmpty(imagePath)) return;
            string tagValue = _selectedButton.Tag as string;
            switch (tagValue)
            {
                case "first":
                    UpdateTeam(1, type, imagePath);
                    break;
                case "second":
                    UpdateTeam(2, type, imagePath);
                    break;
                case "third":
                    UpdateTeam(3, type, imagePath);
                    break;
                default:
                    Debug.WriteLine("Invalid tag value");
                    break;
            }
            UpdateButtonImage(imagePath);
        }
        private void UpdateButtonImage(string imagePath)
        {
            var ellipse = _selectedButton.Template.FindName("ButtonEllipse", _selectedButton) as Ellipse;
            if (ellipse != null)
            {
                ellipse.Fill = new ImageBrush(new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)));
            }
        }
        public void UpdateTeam(int heroNumber, string type, string imagePath)
        {
            switch (type)
            {
                case "hero":
                    UpdateHeroImage(heroNumber, imagePath);
                    SetHeroImagePath(heroNumber, imagePath);
                    break;
                case "echo":
                    UpdateEchoImage(heroNumber, imagePath);
                    SetEchoImagePath(heroNumber, imagePath);
                    break;
                case "elemental":
                    UpdateElementalImage(heroNumber, imagePath);
                    SetElementalImagePath(heroNumber, imagePath);
                    break;
                default:
                    Debug.WriteLine("Invalid type");
                    break;
            }
        }
        private void SetHeroImagePath(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.imagePath = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.imagePath = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.imagePath = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void SetEchoImagePath(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.echo.path = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.echo.path = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.echo.path = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void SetElementalImagePath(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.element.path = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.element.path = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.element.path = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void UpdateHeroImage(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.imagePath = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.imagePath = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.imagePath = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void UpdateEchoImage(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.echo.path = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.echo.path = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.echo.path = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void UpdateElementalImage(int heroNumber, string imagePath)
        {
            switch (heroNumber)
            {
                case 1:
                    currentTeam.firstHero.element.path = imagePath;
                    break;
                case 2:
                    currentTeam.secondHero.element.path = imagePath;
                    break;
                case 3:
                    currentTeam.thirdHero.element.path = imagePath;
                    break;
                default:
                    Debug.WriteLine("Invalid hero number");
                    break;
            }
        }
        private void LoadJsonTeams()
        {
            // Load teams from JSON and ensure uniqueness
            string folderPath = System.IO.Path.Combine(Helper.jsonPath, "Teams");
            string[] teamFiles = Directory.GetFiles(folderPath, "*.json");
            teams.Clear();
            HashSet<string> loadedTeamNames = new HashSet<string>();
            foreach (var teamFile in teamFiles)
            {
                string json = File.ReadAllText(teamFile);
                var team = JsonConvert.DeserializeObject<Team>(json);
                if (team != null && loadedTeamNames.Add(team.name))
                {
                    teams.Add(team);
                }
            }
        }
        private void PopulateComboBox()
        {
            // Clear existing items to avoid duplicates
            TeamComboBox.Items.Clear();
            // Use a HashSet to ensure uniqueness of team names
            HashSet<string> teamNames = new HashSet<string>();
            foreach (var team in teams)
            {
                if (teamNames.Add(team.name))
                {
                    var comboBoxItem = new ComboBoxItem { Content = team.name };
                    TeamComboBox.Items.Add(comboBoxItem);

                    // Set the selected item if the team name matches current team's name
                    if (team.name == currentTeam.name)
                    {
                        TeamComboBox.SelectedItem = comboBoxItem;
                    }
                }
            }
        }
        private void TeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var teamName = selectedItem.Content.ToString();
                currentTeam = teams.FirstOrDefault(t => t.name == teamName);

                UserSettings.UpdateTeam(currentTeam);
                if (currentTeam != null)
                {
                    GlobalEvents.InvokeChangedTeam();
                    UpdateTeamInterface();
                }
            }
        }
        public void SaveTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentTeam != null)
                {
                    if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                    {
                        MessageBox.Show("Team doesnt have name");
                        return;
                    }
                    currentTeam.ResetData();

                    currentTeam.firstHero.name = Helper.ReturnHeroNameWithImagePath(currentTeam.firstHero.imagePath);
                    currentTeam.secondHero.name = Helper.ReturnHeroNameWithImagePath(currentTeam.secondHero.imagePath);
                    currentTeam.thirdHero.name = Helper.ReturnHeroNameWithImagePath(currentTeam.thirdHero.imagePath);
                    currentTeam.name = NameTextBox.Text;
                    currentTeam.description = DescriptionTextBox.Text;
                    string folderPath = System.IO.Path.Combine(Helper.jsonPath, "Teams");
                    string filePath = System.IO.Path.Combine(folderPath, $"{currentTeam.name}.json");
                    string json = JsonConvert.SerializeObject(currentTeam, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    UserSettings.UpdateTeam(currentTeam);
                    MessageBox.Show("Team saved successfully!");
                    LoadList();
                }
                else
                {
                    MessageBox.Show("No team selected to save.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save team: {ex.Message}");
            }
        }
        public void UpdateTeamInterface()
        {

            // Обновление интерфейса для первого героя
            UpdateHeroInterface(FirstHeroButton, currentTeam.firstHero);
            UpdateEchoInterface(FirstEchoButton, currentTeam.firstHero.echo);
            UpdateElementalInterface(FirstElementButton, currentTeam.firstHero.element);
            // Обновление интерфейса для второго героя
            UpdateHeroInterface(SecondHeroButton, currentTeam.secondHero);
            UpdateEchoInterface(SecondEchoButton, currentTeam.secondHero.echo);
            UpdateElementalInterface(SecondElementButton, currentTeam.secondHero.element);
            // Обновление интерфейса для третьего героя
            UpdateHeroInterface(ThirdHeroButton, currentTeam.thirdHero);
            UpdateEchoInterface(ThirdEchoButton, currentTeam.thirdHero.echo);
            UpdateElementalInterface(ThirdElementButton, currentTeam.thirdHero.element);
            NameTextBox.Text = currentTeam.name;
            DescriptionTextBox.Text = currentTeam.description;

            //TeamComboBox.ItemsSource = NameTextBox.Text;
            //  TeamComboBox.DisplayMemberPath = NameTextBox.Text;

            GameStates.Instance.currentTeam = currentTeam;
        }
        private void UpdateHeroInterface(Button heroButton, Character hero)
        {
            // Debug.WriteLine(heroButton.Tag);
            if (heroButton != null && hero != null && !string.IsNullOrEmpty(hero.imagePath))
            {
                var ellipse = heroButton.Template.FindName("ButtonEllipse", heroButton) as Ellipse;
                if (ellipse != null)
                {
                    ellipse.Fill = new ImageBrush(new BitmapImage(new Uri(hero.imagePath, UriKind.RelativeOrAbsolute)));
                }
            }
        }
        private void UpdateEchoInterface(Button heroButton, Ability echo)
        {
            if (heroButton != null && echo != null && !string.IsNullOrEmpty(echo.path))
            {
                var ellipse = heroButton.Template.FindName("ButtonEllipse", heroButton) as Ellipse;
                if (ellipse != null)
                {
                    ellipse.Fill = new ImageBrush(new BitmapImage(new Uri(echo.path, UriKind.RelativeOrAbsolute)));
                }
            }
        }
        private void UpdateElementalInterface(Button heroButton, Ability elemental)
        {
            if (heroButton != null && elemental != null && !string.IsNullOrEmpty(elemental.path))
            {
                var ellipse = heroButton.Template.FindName("ButtonEllipse", heroButton) as Ellipse;
                if (ellipse != null)
                {
                    ellipse.Fill = new ImageBrush(new BitmapImage(new Uri(elemental.path, UriKind.RelativeOrAbsolute)));
                }
            }
        }
    }
}
