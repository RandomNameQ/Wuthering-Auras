using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Wuthering_Waves_comfort_vision.Scripts.Json
{
    public class OverlayCharacterScreenData
    {
        public float UpdateSpeed = 0.05f;
        public List<OverlayImageData> OriginalOverlaySizePos { get; set; } = new List<OverlayImageData>();
        public List<OverlayImageData> NewOverlayCharacterSizePos { get; set; } = new List<OverlayImageData>();

        [NonSerialized]
        private const string FilePath = "pack://application:,,,/json/overlayCharacter.json";


        public void LoadJson()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            if (File.Exists(path))
            {
                try
                {
                    string jsonData = File.ReadAllText(path);
                    var data = JsonConvert.DeserializeObject<OverlayCharacterScreenData>(jsonData);
                    if (data != null)
                    {
                        UpdateSpeed = data.UpdateSpeed;
                        OriginalOverlaySizePos = data.OriginalOverlaySizePos ?? new List<OverlayImageData>();
                        NewOverlayCharacterSizePos = data.NewOverlayCharacterSizePos ?? new List<OverlayImageData>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading JSON: {ex.Message}");
                }
            }
            else
            {
                // Initialize with default values and save to create the file
                UpdateSpeed = 0.05f; // Default value
                SaveJson();
            }

        }

        public void SaveJson()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            try
            {
                string jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(path, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON: {ex.Message}");
            }
        }

        public void UpdateData(bool needOriginalUpdate, List<OverlayImageData> data)
        {
            if (needOriginalUpdate)
            {
                OriginalOverlaySizePos = data;
            }
            else
            {
                NewOverlayCharacterSizePos = data;

            }
            SaveJson();
        }

        // public void UpdateGameState() => GameStates.Instance.characterOverlayData = this;
    }
}
