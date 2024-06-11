using System.Windows;
using Wuthering_Waves_comfort_vision.Scripts.Json;
using Wuthering_Waves_comfort_vision.Scripts.Main;
using Wuthering_Waves_comfort_vision.Xaml;
namespace Wuthering_Waves_comfort_vision
{

    public partial class App : Application
    {
        public HotkeyDetector hotkeyDetector;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EntryPoint();
            //DetectSwapHero pickedHeroes = new DetectSwapHero();
        }
        public void EntryPoint()
        {
            UserSettings userSettings = new UserSettings();
            UnifiedJson unifiedJson = new UnifiedJson();
            // создаю интерфейс HeroList - создаю кнопки-картинки- данные беру из GameStates
            HeroList heroList = new HeroList();
            heroList.LoadHeroButtons();
            // тут загружаем джон оверлевв
            //OverlayCharacterScreenData overlayCharacterScreenData = new OverlayCharacterScreenData();
            //overlayCharacterScreenData.LoadJson();

            hotkeyDetector = new HotkeyDetector();



            // Обязательно надо заупускать от имени админа
        }
    }
}
