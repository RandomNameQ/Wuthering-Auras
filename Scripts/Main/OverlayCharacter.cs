using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using Wuthering_Waves_comfort_vision.Data;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;
namespace Wuthering_Waves_comfort_vision.Scripts.Main
{
    public class OverlayCharacter : IDisposable
    {
        string filePath = System.IO.Path.Combine(Helper.jsonPath, $"CharacterOverlay.json");
        public OverlayArea FirstHero { get; set; } = new OverlayArea();
        public OverlayArea SecondHero { get; set; } = new OverlayArea();
        public OverlayArea ThirdHero { get; set; } = new OverlayArea();
        public OverlayArea UltimateArea1 { get; set; } = new OverlayArea();
        public OverlayArea UltimateArea2 { get; set; } = new OverlayArea();
        public OverlayArea UltimateArea3 { get; set; } = new OverlayArea();

        public OverlayArea Concert1 { get; set; } = new OverlayArea();
        public OverlayArea Concert2 { get; set; } = new OverlayArea();
        public OverlayArea Concert3 { get; set; } = new OverlayArea();

        public OverlayArea Forte1 { get; set; } = new OverlayArea();
        public OverlayArea Forte2 { get; set; } = new OverlayArea();
        public OverlayArea Forte3 { get; set; } = new OverlayArea();

        public List<OverlayArea> overlayAreas = new List<OverlayArea>();
        private System.Windows.Threading.DispatcherTimer timer;
        private List<OverlayImage> overlayImages = new();
        public bool isShowDetectors = false;
        public bool isShowOverlay = false;
        public bool isFirstRender;
        public bool firstLoad = true;
        private bool isTimerRunning;
        public OverlayCharacter()
        {
            GlobalEvents.ChangeRenderState += ChangeRenderState;
            GlobalEvents.TeamHasBeenChanged += UpdateImagesForChangedTeam;
        }
        private void UpdateImagesForChangedTeam()
        {
            StopRenderOverlay();
        }
        public void LoadJson()
        {
            overlayAreas.Clear();
            overlayImages.Clear();

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<dynamic>(jsonData);
                FirstHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.FirstHero));
                SecondHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.SecondHero));
                ThirdHero = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.ThirdHero));

                UltimateArea1 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea1));
                UltimateArea2 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea2));
                UltimateArea3 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.UltimateArea3));

                Concert1 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Concert1));
                Concert2 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Concert2));
                Concert3 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Concert3));

                Forte1 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Forte1));
                Forte2 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Forte2));
                Forte3 = JsonConvert.DeserializeObject<OverlayArea>(Convert.ToString(settings.Forte3));


            }

            UltimateArea1.currentCharacter = GameStates.Instance.currentTeam.firstHero;
            UltimateArea2.currentCharacter = GameStates.Instance.currentTeam.secondHero;
            UltimateArea3.currentCharacter = GameStates.Instance.currentTeam.thirdHero;

            Concert1.currentCharacter = GameStates.Instance.currentTeam.firstHero;
            Concert2.currentCharacter = GameStates.Instance.currentTeam.secondHero;
            Concert3.currentCharacter = GameStates.Instance.currentTeam.thirdHero;

            Forte1.currentCharacter = GameStates.Instance.currentTeam.firstHero;
            Forte2.currentCharacter = GameStates.Instance.currentTeam.secondHero;
            Forte3.currentCharacter = GameStates.Instance.currentTeam.thirdHero;

            overlayAreas.Add(FirstHero);
            overlayAreas.Add(SecondHero);
            overlayAreas.Add(ThirdHero);

            overlayAreas.Add(UltimateArea1);
            overlayAreas.Add(UltimateArea2);
            overlayAreas.Add(UltimateArea3);

            overlayAreas.Add(Concert1);
            overlayAreas.Add(Concert2);
            overlayAreas.Add(Concert3);

            overlayAreas.Add(Forte1);
            overlayAreas.Add(Forte2);
            overlayAreas.Add(Forte3);


            if (overlayImages.Count == 0)
            {
                for (int i = 0; i < overlayAreas.Count; i++)
                {
                    var overlayArea = overlayAreas[i].overlayArea;
                    overlayImages.Add(Helper.CreateImageWindow(overlayArea.height, overlayArea.width, overlayArea.x, overlayArea.y));
                    overlayImages[i].cooldownTimerTextBlock.Text = "";
                    overlayImages[i].Hide();
                }
            }
        }
        public void SaveJson()
        {
            if (overlayImages.Count == 0)
            {
                return;
            }

            UpdateOverlayArea(FirstHero, overlayImages[0]);
            UpdateOverlayArea(SecondHero, overlayImages[1]);
            UpdateOverlayArea(ThirdHero, overlayImages[2]);

            UpdateOverlayArea(UltimateArea1, overlayImages[3], true);
            UpdateOverlayArea(UltimateArea2, overlayImages[4], true);
            UpdateOverlayArea(UltimateArea3, overlayImages[5], true);

            UpdateOverlayArea(Concert1, overlayImages[6], true);
            UpdateOverlayArea(Concert2, overlayImages[7], true);
            UpdateOverlayArea(Concert3, overlayImages[8], true);

            UpdateOverlayArea(Forte1, overlayImages[9], true);
            UpdateOverlayArea(Forte2, overlayImages[10], true);
            UpdateOverlayArea(Forte3, overlayImages[11], true);


            var settings = new
            {
                FirstHero = FirstHero,
                SecondHero = SecondHero,
                ThirdHero = ThirdHero,
                UltimateArea1 = UltimateArea1,
                UltimateArea2 = UltimateArea2,
                UltimateArea3 = UltimateArea3,

                Concert1 = Concert1,
                Concert2 = Concert2,
                Concert3 = Concert3,
                Forte1 = Forte1,
                Forte2 = Forte2,
                Forte3 = Forte3,
            };
            string jsonData = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        private void UpdateOverlayArea(OverlayArea overlayArea, OverlayImage image, bool isStopUpdateIfNewCharacter = false)
        {
            overlayArea.overlayArea.y = (int)image.Top;
            overlayArea.overlayArea.x = (int)image.Left;
            overlayArea.overlayArea.width = (int)image.Width;
            overlayArea.overlayArea.height = (int)image.Height;

            // Since isStopUpdateIfNewCharacter is part of OverlayArea, it should be set separately
            if (overlayArea != null)
            {
                overlayArea.isStopUpdateIfNewCharacter = isStopUpdateIfNewCharacter;
            }

        }

        public void StartRenderOverlay()
        {
            LoadJson();
            StartUpdateTimer();
        }
        public void StopRenderOverlay()
        {
            firstLoad = true;
            StopTimer();
            SaveJson();
        }
        private void StartUpdateTimer()
        {
            isTimerRunning = true;
            if (timer == null)
            {
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                timer.Start();
            }

        }



        private void Timer_Tick(object sender, EventArgs e)
        {

            if (!isTimerRunning)
            {
                return;
            }

            if (GameStates.Instance.appSettings.isRenderIfWutherinfWindow && !GameStates.Instance.isWutheringWavesWindow)
            {

                for (int i = 0; i < overlayAreas.Count; i++)
                {
                    overlayImages[i].Hide();
                }
                return;
            }


            for (int i = 0; i < overlayAreas.Count; i++)
            {
                if (!overlayAreas[i].isOverlayEnable)
                {
                    overlayImages[i].Hide();
                    continue;
                }


                if (overlayAreas[i].isStopUpdateIfNewCharacter)
                {
                    if (GameStates.Instance.currentCharacter != null)
                    {
                        if (GameStates.Instance.currentCharacter.name != overlayAreas[i].currentCharacter.name)
                        {
                            if (!firstLoad)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        overlayImages[i].Hide();
                        continue;
                    }
                }

                var originalArea = overlayAreas[i].originalArea;
                BitmapSource refreshedImage = Helper.CaptureScreen(originalArea.x, originalArea.y, originalArea.width, originalArea.height);
                if (refreshedImage == null)
                {
                    Debug.WriteLine("fail overlay");
                }
                overlayImages[i].UpdateImage(refreshedImage);

                overlayImages[i].Show();

            }
            firstLoad = false;
        }
        private void StopTimer()
        {
            isTimerRunning = false;
            timer?.Stop();
            timer = null;
            for (int i = 0; i < overlayImages.Count; i++)
            {
                overlayImages[i].Close();
                // overlayImages[i].Hide();
            }
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer?.Stop();
                    GlobalEvents.ChangeRenderState -= ChangeRenderState;
                    SaveJson();
                }
                disposedValue = true;
            }
        }
        ~OverlayCharacter()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void ChangeRenderState(bool isRenderNow)
        {

            if (isRenderNow)
            {
                if (timer != null)
                {
                    PauseTimer();

                }
            }
            else
            {
                if (timer != null)
                {
                    ResumeTimer();
                }
            }
        }
        private void PauseTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                for (int i = 0; i < overlayImages.Count; i++)
                {
                    overlayImages[i].Hide();
                }
            }
        }
        private void ResumeTimer()
        {
            if (timer != null && !timer.IsEnabled)
            {
                timer.Start();
            }
        }
    }
}
