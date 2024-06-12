using System.Collections.Generic;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Main.Hotkeys
{
    public class HotkeyCollector
    {
        public Dictionary<Ability, KeyGesture> BuffsHotkeys { get; private set; } = new();
        public Dictionary<Ability, KeyGesture> CooldownHotkeys { get; private set; } = new();

        public void Collect()
        {
            if (GameStates.Instance.currentTeam == null)
            {
                return;
            }
            GameStates.Instance.currentTeam.firstHero.isAvaible = true;
            GameStates.Instance.currentTeam.secondHero.isAvaible = true;
            GameStates.Instance.currentTeam.thirdHero.isAvaible = true;
            BuffsHotkeys.Clear();
            CooldownHotkeys.Clear();
            List<Ability> skills = new();
            var team = GameStates.Instance.currentTeam;
            var first = team.firstHero;
            var second = team.secondHero;
            var third = team.thirdHero;
            AddBuffHotKeyIfRenderable(first);
            AddBuffHotKeyIfRenderable(second);
            AddBuffHotKeyIfRenderable(third);
            AddCooldownHotKeyIfRenderable(first);
            AddCooldownHotKeyIfRenderable(second);
            AddCooldownHotKeyIfRenderable(third);
        }

        private void AddBuffHotKeyIfRenderable(Character character)
        {
            AddHotKeyToDictionary(character.intro, BuffsHotkeys);
            AddHotKeyToDictionary(character.echo, BuffsHotkeys);
            AddHotKeyToDictionary(character.resonance, BuffsHotkeys);
            AddHotKeyToDictionary(character.ultimate, BuffsHotkeys);
            AddHotKeyToDictionary(character.outro, BuffsHotkeys);
            AddHotKeyToDictionary(character.element, BuffsHotkeys);
            AddHotKeyToDictionary(character.inherit, BuffsHotkeys);
            AddHotKeyToDictionary(character.weapon, BuffsHotkeys);
        }

        private void AddCooldownHotKeyIfRenderable(Character character)
        {
            AddHotKeyToDictionary(character.cooldownIntro, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownEcho, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownResonance, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownUltimate, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownOutro, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownElement, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownInherit, CooldownHotkeys);
            AddHotKeyToDictionary(character.cooldownWeapon, CooldownHotkeys);
        }

        private void AddHotKeyToDictionary(Ability ability, Dictionary<Ability, KeyGesture> dictionary)
        {
            if (ability.isCanRenderImage && !string.IsNullOrEmpty(ability.hotKeyToActivate))
            {
                var keyGesture = ConvertStringToKeyGesture(ability.hotKeyToActivate);
                if (keyGesture == null)
                {
                    var modifiedHotKey = $"Windows+{ability.hotKeyToActivate}";
                    keyGesture = ConvertStringToKeyGesture(modifiedHotKey);
                }
                if (keyGesture != null)
                {
                    if (keyGesture.Modifiers == ModifierKeys.None)
                    {
                        keyGesture = new KeyGesture(keyGesture.Key, ModifierKeys.Windows);
                    }
                    dictionary.Add(ability, keyGesture);
                }
            }
        }

        private KeyGesture ConvertStringToKeyGesture(string gestureString)
        {
            try
            {
                var keyGestureConverter = new KeyGestureConverter();
                return (KeyGesture)keyGestureConverter.ConvertFromString(gestureString);
            }
            catch
            {
                // Логирование ошибки при необходимости
                return null;
            }
        }
    }
}

