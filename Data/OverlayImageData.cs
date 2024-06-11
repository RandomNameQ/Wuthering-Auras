using System;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;

public class OverlayImageData
{
    public int width = 50, height = 50;
    public int x, y;
    public int tranparency;
    [NonSerialized]
    public OverlayImage overlayImage;


    public void SaveOverlayData()
    {
        if (overlayImage == null)
        {
            return;
        }
        x = (int)overlayImage.Left;
        y = (int)overlayImage.Top;
        width = (int)overlayImage.Width;
        height = (int)overlayImage.Height;
    }
}