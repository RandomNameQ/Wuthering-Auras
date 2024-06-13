namespace Wuthering_Waves_comfort_vision.Scripts.Main
{
    internal class VisualStatus
    {
        public class States
        {
            public int x, y;
            public int width, height;
            public string path;

            public enum VariantState
            {
                isAutoLoot,
                isAutoW,
                isAutoA,
                isAutoD,
            }

        }
    }
}
