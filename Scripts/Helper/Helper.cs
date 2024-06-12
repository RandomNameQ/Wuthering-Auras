using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;

public static class Helper
{

    public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
    public static string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "json");

    public static string GetPathImage(string name, string type)
    {
        switch (type.ToLower())
        {
            case "hero":
                return GetHeroPathImage(name);
            case "echo":
                return GetEchoPathImage(name);
            case "elemental":
                return GetElementalPathImage(name);
            default:
                return default;
        }
    }
    public static string GetHeroPathImage(string heroName)
    {
        for (int i = 0; i < GameStates.Instance.Characters.Count; i++)
        {
            var hero = GameStates.Instance.Characters[i];
            if (heroName == hero.name)
            {
                return hero.imagePath;
            }
        }
        return default;
    }
    public static string GetEchoPathImage(string echoName)
    {
        for (int i = 0; i < GameStates.Instance.Echos.Count; i++)
        {
            var echo = GameStates.Instance.Echos[i];
            if (echoName == echo.name)
            {
                return echo.skill.path;
            }
        }
        return default;
    }
    public static string GetElementalPathImage(string elementalName)
    {
        for (int i = 0; i < GameStates.Instance.Elemental.Count; i++)
        {
            var elemental = GameStates.Instance.Elemental[i];
            if (elementalName == elemental.name)
            {
                return elemental.skill.path;
            }
        }
        return default;
    }
    public static ImageSource LoadImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
        {
            throw new ArgumentException("Invalid image path provided.");
        }

        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.UriSource = new Uri(imagePath, UriKind.Absolute);
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        bitmapImage.Freeze(); // Make it cross-thread accessible

        return bitmapImage;
    }
    public static BitmapSource CaptureScreen(int x, int y, int width, int height)
    {
        Bitmap bitmap = new Bitmap(width, height);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy);
        }

        IntPtr hBitmap = bitmap.GetHbitmap();
        BitmapSource bitmapSource;

        try
        {
            bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
            DeleteObject(hBitmap);
            bitmap.Dispose();
        }

        return bitmapSource;
    }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);

    public static OverlayImage CreateImageWindow(int height, int width, int x, int y, bool needSaveAfterChangePos = false)
    {
        var overlayImage = new OverlayImage
        {
            Width = width,
            Height = height,
            Left = x,
            Top = y,
            WindowStartupLocation = WindowStartupLocation.Manual,
            WindowStyle = WindowStyle.None,
            //ResizeMode = ResizeMode.CanResizeWithGrip,

            AllowsTransparency = true,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 255, 255, 255)),
            Topmost = true,
            ShowInTaskbar = false

        };

        if (needSaveAfterChangePos)
        {
            overlayImage.MouseDown += overlayImage.SaveDataAfterChangedPositionOrSize;
        }

        if (GameStates.Instance.appSettings.isCanControlPositionSizeIcon)
        {
            overlayImage.Subscribe();
        }
        else
        {
            overlayImage.UnSubscribe();
        }

        //overlayImage.MouseDown += overlayImage.OverlayImage_MouseDown;
        //overlayImage.MouseMove += overlayImage.OverlayImage_MouseMove;
        overlayImage.Show();

        // возвращаепм окно, чтобы мочь его закрывать 
        return overlayImage;
    }


    public static OverlayImage CreateImageForCooldown(int height, int width, int x, int y, bool needSaveAfterChangePos = false)
    {
        var overlayImage = new OverlayImage
        {
            Width = width,
            Height = height,
            Left = x,
            Top = y,
            WindowStartupLocation = WindowStartupLocation.Manual,
            WindowStyle = WindowStyle.None,
            ResizeMode = ResizeMode.CanResizeWithGrip,
            AllowsTransparency = true,
            Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 255, 255)),
            Topmost = true

        };

        if (needSaveAfterChangePos)
        {
            overlayImage.MouseDown += overlayImage.SaveDataAfterChangedPositionOrSize;
        }

        overlayImage.darkOverlay.Height = 100;

        overlayImage.MouseDown += overlayImage.OverlayImage_MouseDown;
        overlayImage.MouseMove += overlayImage.OverlayImage_MouseMove;
        overlayImage.Show();

        // возвращаепм окно, чтобы мочь его закрывать 
        return overlayImage;
    }


    public static string ReturnHeroNameWithImagePath(string imagePath)
    {
        foreach (var character in GameStates.Instance.Characters)
        {
            if (character.imagePath == imagePath)
            {
                return character.name;
            }
        }
        return default;
    }
}

