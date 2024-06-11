
using Newtonsoft.Json;
using System.IO;
using Wuthering_Waves_comfort_vision.Data.Team;

public class UserSettings
{
    private static Team lastChosen;
    public static string filePath = Path.Combine(Helper.jsonPath, "UserSettings.json");
    public UserSettings()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        if (File.Exists(filePath) && GameStates.Instance.currentTeam == null)
        {
            string json = File.ReadAllText(filePath);
            var settings = JsonConvert.DeserializeObject<UserSettingsData>(json);
            lastChosen = settings?.LastChosen;
            GameStates.Instance.currentTeam = lastChosen;
            GameStates.Instance.currentCharacter = lastChosen?.firstHero;

        }
        else
        {
            CreateJson();
        }
    }

    public void CreateJson()
    {
        if (!File.Exists(filePath))
        {

            var settings = new UserSettingsData { LastChosen = FindAnyTeam() };
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            GameStates.Instance.currentTeam = settings.LastChosen;
            File.WriteAllText(filePath, json);
        }
    }
    public Team FindAnyTeam()
    {
        string folderPath = System.IO.Path.Combine(Helper.jsonPath, "Teams");
        string[] teamFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (var teamFile in teamFiles)
        {
            string json = File.ReadAllText(teamFile);
            var team = JsonConvert.DeserializeObject<Team>(json);

            return team;
        }

        return null;
    }



    public static void UpdateTeam(Team newTeam)
    {
        lastChosen = newTeam;
        UpdateJson();
    }

    public static Team GetTeam()
    {

        return lastChosen;
    }

    public static void UpdateJson()
    {
        var settings = new UserSettingsData { LastChosen = lastChosen };
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public Team GetLastChosenTeam()
    {
        return lastChosen;
    }


}

public class UserSettingsData
{
    public Team LastChosen { get; set; }
}