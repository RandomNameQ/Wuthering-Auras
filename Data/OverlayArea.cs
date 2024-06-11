using System;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;

namespace Wuthering_Waves_comfort_vision.Data
{
    public class OverlayArea
    {
        public ImagesData overlayArea;
        public ImagesData originalArea;

        public float screenRefreshRate;
        public bool isStopUpdateIfNewCharacter;
        public bool isOverlayEnable;
        public int number;
        [NonSerialized]
        public Character currentCharacter;

        public OverlayArea()
        {
            OverlaySettings();
            OriginalSettings();
            if (screenRefreshRate == 0)
            {
                screenRefreshRate = 0.1f;
            }
        }

        public void OriginalSettings()
        {
            originalArea = new();
            if (originalArea.height == 0 && originalArea.width == 0)
            {
                originalArea.height = 50;
                originalArea.width = 50;
            }

        }
        public void OverlaySettings()
        {
            overlayArea = new();

            if (overlayArea.height == 0 && overlayArea.width == 0)
            {
                overlayArea.height = 50;
                overlayArea.width = 50;
            }

        }


        public class ImagesData
        {
            public int x, y, width, height;

            [NonSerialized]
            public Character originalCharacter;
            [NonSerialized]
            public OverlayImage overlayImage;
            [NonSerialized]
            public bool isCreated;
        }
    }
}
