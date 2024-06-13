using System.Windows;
using System.Windows.Media;

namespace Wuthering_Waves_comfort_vision.Xaml.Main
{
    /// <summary>
    /// Interaction logic for OverlayIcon.xaml
    /// </summary>
    public partial class OverlayIcon : Window
    {
        public OverlayIcon()
        {
            InitializeComponent();


        }

        public class Data
        {
            public int x, y;
            public int width, height;
            public Color borderColor;
        }
    }
}
