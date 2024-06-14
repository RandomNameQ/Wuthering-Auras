using System.Collections.Generic;
using Wuthering_Waves_comfort_vision;
using Wuthering_Waves_comfort_vision.Data.Elemental;
using Wuthering_Waves_comfort_vision.Data.Team;
using Wuthering_Waves_comfort_vision.Xaml.Main;

public class GameStates
{
    private static GameStates _instance;
    public static GameStates Instance => _instance ??= new GameStates();
    public List<Character> Characters { get; set; } = new List<Character>();
    public List<Echo> Echos { get; set; } = new List<Echo>();
    public List<Elemental> Elemental { get; set; } = new List<Elemental>();
    public Team currentTeam;
    public bool isGameWindow;
    public AppSettings appSettings = new();

    public Character currentCharacter;
    private GameStates()
    {
    }

    public QOL.HotkeysSettings qolHotkey = new();
}
