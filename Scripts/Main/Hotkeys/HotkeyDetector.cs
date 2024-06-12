using Gma.System.MouseKeyHook;
using System.Collections.Generic;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Main.Hotkeys
{
    public class HotkeyDetector
    {
        private SwapCharacter swapCharacter;
        private QOL_Hotkeys qol_Hotkeys;
        private IKeyboardMouseEvents globalHook;
        private List<Key> hotkeySwitchRender;

        public HotkeyCollector HotkeyCollector { get; private set; }
        public bool NeedPause { get; set; }
        public Buffs_Hotkey BuffsHotkey { get; private set; }

        public HotkeyDetector()
        {
            swapCharacter = new SwapCharacter();
            qol_Hotkeys = new QOL_Hotkeys();
            HotkeyCollector = new HotkeyCollector();
            hotkeySwitchRender = new List<Key> { Key.F11, Key.Oem3 };

            BuffsHotkey = new Buffs_Hotkey(this);

            HotkeyCollector.Collect();
            Subscribe();
        }

        private void Subscribe()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += OnGlobalHookKeyDown;
        }

        ~HotkeyDetector()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            if (globalHook != null)
            {
                globalHook.KeyDown -= OnGlobalHookKeyDown;
                globalHook.Dispose();
                globalHook = null;
            }
        }

        private void OnGlobalHookKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!GameStates.Instance.appSettings.isDetectHotkey) return;

            if (GameStates.Instance.appSettings.isWutheringWindow && !GameStates.Instance.isGameWindow)
            {
                return;
            }

            Key key = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            CheckInput(key, Keyboard.Modifiers);
        }

        public void CheckInput(Key key, ModifierKeys modifiers)
        {
            BuffsHotkey.Buff_Input(key, modifiers);
            BuffsHotkey.Cooldown_Input(key, modifiers);

            swapCharacter.ChangeCharacter(key);
            qol_Hotkeys.RedirectHotkeys(key);
            qol_Hotkeys.AutoEvents(key, modifiers);

            HandleSwitchRenderStateInput(key);
        }

        private void HandleSwitchRenderStateInput(Key key)
        {
            if (hotkeySwitchRender.Contains(key))
            {
                GlobalEvents.InvokeRenderState();
            }
        }
    }
}
