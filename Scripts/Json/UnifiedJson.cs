using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Wuthering_Waves_comfort_vision.Data;
using Wuthering_Waves_comfort_vision.Data.Elemental;
using static Wuthering_Waves_comfort_vision.Xaml.Main.QOL;

namespace Wuthering_Waves_comfort_vision.Scripts.Json
{
    internal class UnifiedJson
    {
        string baseFolderPath = AppDomain.CurrentDomain.BaseDirectory;


        string characterFolderPath = Path.Combine(Helper.appPath, "Assets", "Characters");
        string echoFolderPath = Path.Combine(Helper.appPath, "Assets", "Echo");
        string elementFolderPath = Path.Combine(Helper.appPath, "Assets", "Elemental");





        string echoJsonPath = Path.Combine(Helper.jsonPath, "Echos.json");
        string elementalJsonPath = Path.Combine(Helper.jsonPath, "Elemental.json");
        string characterJsonPath = Path.Combine(Helper.jsonPath, "Characters.json");



        List<UnifiedData> data = new List<UnifiedData>();
        public GameElement currentElement;

        public class UnifiedData
        {
            public string path;
            public string name;
            public ImageSource image;
        }

        public UnifiedJson()
        {
            EchoJson();
            CharactersJson();
            ElementalJson();
            LoadHotkeysSettings();
        }

        public void EchoJson()
        {
            data.Clear();
            currentElement = GameElement.Echo;
            GetImages(echoFolderPath);
            TryCreate<Echo, Wuthering_Waves_comfort_vision.Data.Echo.EchoVariant>(echoJsonPath);
            LoadJson<Echo>(echoJsonPath, GameElement.Echo);
        }

        public void CharactersJson()
        {
            data.Clear();
            currentElement = GameElement.Character;
            GetImages(characterFolderPath);
            TryCreate<Character, Wuthering_Waves_comfort_vision.Data.Hero.CharacterVariant>(characterJsonPath);
            LoadJson<Character>(characterJsonPath, GameElement.Character);
        }

        public void ElementalJson()
        {
            data.Clear();
            currentElement = GameElement.Element;
            GetImages(elementFolderPath);
            TryCreate<Elemental, Wuthering_Waves_comfort_vision.Data.Elemental.ElementalVariant>(elementalJsonPath);
            LoadJson<Elemental>(elementalJsonPath, GameElement.Element);
        }

        public void GetImages(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath, "*.png");
                foreach (var file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    var image = Helper.LoadImage(file);
                    data.Add(new UnifiedData { path = file, name = fileName, image = image });
                }
            }
            else
            {
                Console.WriteLine($"Directory '{folderPath}' does not exist.");
            }
        }

        public void TryCreate<TState, TEnum>(string filePath) where TState : new() where TEnum : Enum
        {
            if (File.Exists(filePath))
            {
                Update<TState, TEnum>(filePath);
            }
            else
            {
                Create<TState, TEnum>(filePath);
            }
        }

        public void Create<TState, TEnum>(string filePath) where TState : new() where TEnum : Enum
        {
            List<TState> stateList = new List<TState>();
            foreach (var dataItem in data)
            {
                dynamic state = new TState();

                // Use reflection to set the properties based on type
                if (typeof(TState) == typeof(Character))
                {
                    SetCharacterProperties(state, dataItem);
                }
                else if (typeof(TState) == typeof(Echo))
                {
                    SetEchoProperties(state, dataItem);
                }
                else if (typeof(TState) == typeof(Elemental))
                {
                    SetElementalProperties(state, dataItem);
                }


                // Set the common name property based on the enum
                foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
                {
                    if (enumValue.ToString().Equals(dataItem.name, StringComparison.OrdinalIgnoreCase))
                    {
                        state.name = enumValue.ToString();
                        break;
                    }
                }

                stateList.Add(state);
            }

            UpdateData(stateList);
            var json = JsonConvert.SerializeObject(stateList, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void SetCharacterProperties(Character character, UnifiedJson.UnifiedData dataItem)
        {
            character.imagePath = dataItem.path;
            character.image = dataItem.image;
            character.name = dataItem.path;
        }

        private void SetEchoProperties(Echo echo, UnifiedJson.UnifiedData dataItem)
        {
            echo.skill.path = dataItem.path;
            echo.skill.image = dataItem.image;

        }
        private void SetElementalProperties(Elemental elemental, UnifiedJson.UnifiedData dataItem)
        {
            elemental.skill.path = dataItem.path;
            elemental.skill.image = dataItem.image;

        }


        public void LoadJson<TState>(string filePath, GameElement gameElement)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var stateList = JsonConvert.DeserializeObject<List<TState>>(json);
                UpdateData(stateList);
            }
            else
            {
                Console.WriteLine($"File '{filePath}' does not exist.");
            }
        }

        public void Update<TState, TEnum>(string filePath) where TState : new() where TEnum : Enum
        {
            string existingJson = File.ReadAllText(filePath);
            JArray existingData = JArray.Parse(existingJson);
            var properties = typeof(TState).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            JArray newData = JArray.FromObject(data);

            foreach (var newItem in newData)
            {
                var existingItem = existingData.FirstOrDefault(item => item["name"]?.ToString() == newItem["name"]?.ToString()) as JObject;

                if (existingItem != null)
                {
                    foreach (var property in properties)
                    {
                        if (existingItem[property.Name] == null)
                        {
                            existingItem[property.Name] = JToken.FromObject(property.GetValue(new TState()));
                        }
                    }
                }
                else
                {
                    var newItemObject = new JObject();
                    foreach (var property in properties)
                    {
                        newItemObject[property.Name] = JToken.FromObject(property.GetValue(new TState()));
                    }
                    newItemObject["name"] = newItem["name"];
                    newItemObject["pathImage"] = newItem["path"];
                    existingData.Add(newItemObject);
                }
            }

            string updatedJson = JsonConvert.SerializeObject(existingData, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }

        public void UpdateData<TState>(List<TState> stateList)
        {
            switch (currentElement)
            {
                case GameElement.Character:
                    GameStates.Instance.Characters = stateList as List<Character>;
                    break;
                case GameElement.Echo:
                    GameStates.Instance.Echos = stateList as List<Echo>;
                    break;
                case GameElement.Element:
                    GameStates.Instance.Elemental = stateList as List<Elemental>;
                    break;
            }
        }

        public void UpdateName()
        {

        }





        private string filePath = Path.Combine(Helper.jsonPath, $"Hotkeys.json");

        private void LoadHotkeysSettings()
        {
            // Check if the settings file exists
            if (File.Exists(filePath))
            {
                // Load JSON from file and deserialize into hotkeys settings object
                string json = File.ReadAllText(filePath);
                GameStates.Instance.qolHotkey = JsonConvert.DeserializeObject<HotkeysSettings>(json);


            }
        }

    }
}
