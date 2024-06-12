using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Main.Hotkeys
{
    public class SwapCharacter
    {
        private Timer firstHeroTimer;
        private Timer secondHeroTimer;
        private Timer thirdHeroTimer;
        private Character prevCharacter;

        public void ChangeCharacter(Key key)
        {
            if (key == Key.D1 && GameStates.Instance.currentTeam.firstHero.isAvaible && GameStates.Instance.currentCharacter != GameStates.Instance.currentTeam.firstHero)
            {
                SwapHero(GameStates.Instance.currentTeam.firstHero);
            }
            else if (key == Key.D2 && GameStates.Instance.currentTeam.secondHero.isAvaible && GameStates.Instance.currentCharacter != GameStates.Instance.currentTeam.secondHero)
            {
                SwapHero(GameStates.Instance.currentTeam.secondHero);
            }
            else if (key == Key.D3 && GameStates.Instance.currentTeam.thirdHero.isAvaible && GameStates.Instance.currentCharacter != GameStates.Instance.currentTeam.thirdHero)
            {
                SwapHero(GameStates.Instance.currentTeam.thirdHero);
            }
        }

        private void SwapHero(Character hero)
        {
            if (GameStates.Instance.currentCharacter != null)
            {
                prevCharacter = GameStates.Instance.currentCharacter;
                StartCooldownTimer(prevCharacter);
                Debug.WriteLine($"Available: {GameStates.Instance.currentCharacter.name}");

            }
            GameStates.Instance.currentCharacter = hero;
        }

        private void StartCooldownTimer(Character hero)
        {
            hero.isAvaible = false;
            if (hero == GameStates.Instance.currentTeam.firstHero)
            {
                firstHeroTimer?.Dispose();
                firstHeroTimer = new Timer(CooldownCallback, hero, 1100, Timeout.Infinite); // 1 second cooldown
            }
            else if (hero == GameStates.Instance.currentTeam.secondHero)
            {
                secondHeroTimer?.Dispose();
                secondHeroTimer = new Timer(CooldownCallback, hero, 1100, Timeout.Infinite); // 1 second cooldown
            }
            else if (hero == GameStates.Instance.currentTeam.thirdHero)
            {
                thirdHeroTimer?.Dispose();
                thirdHeroTimer = new Timer(CooldownCallback, hero, 1100, Timeout.Infinite); // 1 second cooldown
            }
        }

        private void CooldownCallback(object state)
        {
            Character hero = (Character)state;
            hero.isAvaible = true;
        }
    }
}
