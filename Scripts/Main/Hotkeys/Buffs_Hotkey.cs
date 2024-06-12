using System.Collections.Generic;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Main.Hotkeys
{
    public class Buffs_Hotkey
    {

        public Dictionary<Ability, KeyGesture> buffsHotkeys = new();
        public Dictionary<Ability, KeyGesture> cooldownHotkeys = new();
        HotkeyDetector hotkeyDetector;

        public Buffs_Hotkey(HotkeyDetector hotkeyDetector)
        {
            this.hotkeyDetector = hotkeyDetector;


            buffsHotkeys = hotkeyDetector.HotkeyCollector.BuffsHotkeys;
            cooldownHotkeys = hotkeyDetector.HotkeyCollector.CooldownHotkeys;
        }
        public void Buff_Input(Key key, ModifierKeys modifiers)
        {
            foreach (var buffEntry in buffsHotkeys)
            {
                if (buffEntry.Value.Key == key && buffEntry.Value.Modifiers == ModifierKeys.Windows && Keyboard.Modifiers != ModifierKeys.Alt)
                {
                    if (buffEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        buffEntry.Key.renderBuffs.StartRender();
                    }
                }
                if (buffEntry.Value.Key == key && buffEntry.Value.Modifiers == modifiers)
                {
                    if (buffEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        buffEntry.Key.renderBuffs.StartRender();
                    }
                }
            }
        }

        public void Cooldown_Input(Key key, ModifierKeys modifiers)
        {
            foreach (var cooldownEntry in cooldownHotkeys)
            {
                if (cooldownEntry.Value.Key == key && cooldownEntry.Value.Modifiers == ModifierKeys.Windows && Keyboard.Modifiers != ModifierKeys.Alt)
                {
                    if (cooldownEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        cooldownEntry.Key.renderCooldown.StartRender();
                    }
                }
                if (cooldownEntry.Value.Key == key && cooldownEntry.Value.Modifiers == modifiers)
                {
                    if (cooldownEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        cooldownEntry.Key.renderCooldown.StartRender();
                    }
                }
            }
        }



        public void PreRenderAllImage(bool needrender)
        {
            foreach (var buffEntry in buffsHotkeys)
            {
                if (needrender)
                {
                    buffEntry.Key.renderBuffs.StartRender();
                }
                else
                {
                    buffEntry.Key.renderBuffs.StopRender();
                }
            }
            foreach (var cooldownEntry in cooldownHotkeys)
            {
                if (needrender)
                {
                    cooldownEntry.Key.renderCooldown.StartRender();
                }
                else
                {
                    cooldownEntry.Key.renderCooldown.StopRender();
                }
            }
        }
    }
}
