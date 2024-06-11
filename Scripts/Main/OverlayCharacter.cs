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
        public List<OverlayArea> overlayAreas = new List<OverlayArea>();
        private System.Windows.Threading.DispatcherTimer timer;
        private List<OverlayImage> overlayImages = new();
        public bool isShowDetectors = false;
        public bool isShowOverlay = false;
        public bool isFirstRender;
        public bool firstLoad = true;
        public OverlayCharacter()
        {
            GlobalEvents.ChangeRenderState += ChangeRenderState;
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
            }

            UltimateArea1.currentCharacter = GameStates.Instance.currentTeam.firstHero;
            UltimateArea2.currentCharacter = GameStates.Instance.currentTeam.secondHero;
            UltimateArea3.currentCharacter = GameStates.Instance.currentTeam.thirdHero;
            overlayAreas.Add(FirstHero);
            overlayAreas.Add(SecondHero);
            overlayAreas.Add(ThirdHero);
            overlayAreas.Add(UltimateArea1);
            overlayAreas.Add(UltimateArea2);
            overlayAreas.Add(UltimateArea3);
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
            var image = overlayImages[0];
            FirstHero.overlayArea.y = (int)image.Top;
            FirstHero.overlayArea.x = (int)image.Left;
            FirstHero.overlayArea.width = (int)image.Width;
            FirstHero.overlayArea.height = (int)image.Height;
            image = overlayImages[1];
            SecondHero.overlayArea.y = (int)image.Top;
            SecondHero.overlayArea.x = (int)image.Left;
            SecondHero.overlayArea.width = (int)image.Width;
            SecondHero.overlayArea.height = (int)image.Height;
            image = overlayImages[2];
            ThirdHero.overlayArea.y = (int)image.Top;
            ThirdHero.overlayArea.x = (int)image.Left;
            ThirdHero.overlayArea.width = (int)image.Width;
            ThirdHero.overlayArea.height = (int)image.Height;
            image = overlayImages[3];
            UltimateArea1.overlayArea.y = (int)image.Top;
            UltimateArea1.overlayArea.x = (int)image.Left;
            UltimateArea1.overlayArea.width = (int)image.Width;
            UltimateArea1.overlayArea.height = (int)image.Height;
            UltimateArea1.isStopUpdateIfNewCharacter = true;
            image = overlayImages[4];
            UltimateArea2.overlayArea.y = (int)image.Top;
            UltimateArea2.overlayArea.x = (int)image.Left;
            UltimateArea2.overlayArea.width = (int)image.Width;
            UltimateArea2.overlayArea.height = (int)image.Height;
            UltimateArea2.isStopUpdateIfNewCharacter = true;
            image = overlayImages[5];
            UltimateArea3.overlayArea.y = (int)image.Top;
            UltimateArea3.overlayArea.x = (int)image.Left;
            UltimateArea3.overlayArea.width = (int)image.Width;
            UltimateArea3.overlayArea.height = (int)image.Height;
            UltimateArea3.isStopUpdateIfNewCharacter = true;
            var settings = new
            {
                FirstHero = FirstHero,
                SecondHero = SecondHero,
                ThirdHero = ThirdHero,
                UltimateArea1 = UltimateArea1,
                UltimateArea2 = UltimateArea2,
                UltimateArea3 = UltimateArea3
            };
            string jsonData = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
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
            if (timer == null)
            {
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                ResumeTimer();
            }
        }



        private void Timer_Tick(object sender, EventArgs e)
        {



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
            timer?.Stop();
            for (int i = 0; i < overlayImages.Count; i++)
            {
                //overlayImages[i].Close();
                overlayImages[i].Hide();
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
                StopTimer();
            }
            else
            {
                ResumeTimer();
            }
        }
    }
}
