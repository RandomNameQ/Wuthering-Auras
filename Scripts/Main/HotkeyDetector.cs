using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Timer = System.Threading.Timer;
namespace Wuthering_Waves_comfort_vision.Scripts.Main
{
    public class HotkeyDetector
    {
        private Timer firstHeroTimer;
        private Timer secondHeroTimer;
        private Timer thirdHeroTimer;
        private Character prevCharacter;
        private IKeyboardMouseEvents m_GlobalHook;
        public Dictionary<Ability, KeyGesture> buffsHotkeys = new();
        public Dictionary<Ability, KeyGesture> cooldownHotkeys = new();
        public List<Key> hotkeySwitchRender = new List<Key>
{
    Key.F11,
    Key.Oem3
};
        public bool needPause;
        public void CollectAllHotkeys()
        {
            if (GameStates.Instance.currentTeam == null)
            {
                return;
            }
            GameStates.Instance.currentTeam.firstHero.isAvaible = true;
            GameStates.Instance.currentTeam.secondHero.isAvaible = true;
            GameStates.Instance.currentTeam.thirdHero.isAvaible = true;
            buffsHotkeys.Clear();
            cooldownHotkeys.Clear();
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
            void AddBuffHotKeyIfRenderable(Character character)
            {
                AddHotKeyToDictionary(character.intro, buffsHotkeys);
                AddHotKeyToDictionary(character.echo, buffsHotkeys);
                AddHotKeyToDictionary(character.resonance, buffsHotkeys);
                AddHotKeyToDictionary(character.ultimate, buffsHotkeys);
                AddHotKeyToDictionary(character.outro, buffsHotkeys);
                AddHotKeyToDictionary(character.element, buffsHotkeys);
                AddHotKeyToDictionary(character.inherit, buffsHotkeys);
                AddHotKeyToDictionary(character.weapon, buffsHotkeys);
            }
            void AddCooldownHotKeyIfRenderable(Character character)
            {
                AddHotKeyToDictionary(character.cooldownIntro, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownEcho, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownResonance, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownUltimate, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownOutro, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownElement, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownInherit, cooldownHotkeys);
                AddHotKeyToDictionary(character.cooldownWeapon, cooldownHotkeys);
            }
            void AddHotKeyToDictionary(Ability ability, Dictionary<Ability, KeyGesture> dictionary)
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
                        // Create a new KeyGesture with the Windows modifier if no modifiers are specified
                        if (keyGesture.Modifiers == ModifierKeys.None)
                        {
                            keyGesture = new KeyGesture(keyGesture.Key, ModifierKeys.Windows);
                        }
                        dictionary.Add(ability, keyGesture);
                    }
                }
            }
            KeyGesture ConvertStringToKeyGesture(string gestureString)
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
        #region subscriber
        public HotkeyDetector()
        {
            CollectAllHotkeys();
            Subscribe();
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
        private void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            //m_GlobalHook.MouseWheelExt += OnMouseWheel;
        }



        ~HotkeyDetector()
        {
            Unsubscribe();
        }
        private void Unsubscribe()
        {
            if (m_GlobalHook != null)
            {
                m_GlobalHook.KeyDown -= GlobalHookKeyDown;
                //m_GlobalHook.MouseWheelExt -= OnMouseWheel;
                m_GlobalHook.Dispose();
                m_GlobalHook = null;
            }
        }
        #endregion
        private void GlobalHookKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!GameStates.Instance.appSettings.isDetectHotkey) return;
            if (GameStates.Instance.appSettings.isWutheringWindow)
            {
                if (!GameStates.Instance.isWutheringWavesWindow)
                {
                    return;
                }
            }
            Key convertedKey = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            CheckInput(convertedKey, Keyboard.Modifiers);

        }
        public void CheckInput(Key key, ModifierKeys modifiers)
        {
            // Получение текущих модификаторов
            ModifierKeys currentModifiers = Keyboard.Modifiers;
            // Перебор всех записей в словаре
            Buff_Input(key, modifiers, currentModifiers);
            Cooldown_Input(key, modifiers);


            SwitchRenderState_Input(key);
            ChangeCharacter(key);
            AutoEvents(key, modifiers);
            RedirectHotkeys(key);
        }
        private void SwitchRenderState_Input(Key key)
        {
            for (int i = 0; i < hotkeySwitchRender.Count; i++)
            {
                var hotkey = hotkeySwitchRender[i];
                if (hotkey == key) GlobalEvents.InvokeRenderState();
                break;
            }
        }
        private void Buff_Input(Key key, ModifierKeys modifiers, ModifierKeys currentModifiers)
        {
            foreach (var buffEntry in buffsHotkeys)
            {
                // only one KEY
                if (buffEntry.Value.Key == key && buffEntry.Value.Modifiers == ModifierKeys.Windows && Keyboard.Modifiers != ModifierKeys.Alt)
                {
                    if (buffEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        buffEntry.Key.renderBuffs.StartRender();
                    }
                }
                // only MODIFICATOR+KEY
                if (buffEntry.Value.Key == key && buffEntry.Value.Modifiers == modifiers)
                {
                    if (buffEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        buffEntry.Key.renderBuffs.StartRender();
                    }
                }
            }
        }
        private void Cooldown_Input(Key key, ModifierKeys modifiers)
        {
            Debug.WriteLine(Keyboard.Modifiers);
            foreach (var cooldownEntry in cooldownHotkeys)
            {
                // only one KEY
                if (cooldownEntry.Value.Key == key && cooldownEntry.Value.Modifiers == ModifierKeys.Windows && Keyboard.Modifiers != ModifierKeys.Alt)
                {
                    if (cooldownEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        cooldownEntry.Key.renderCooldown.StartRender();
                    }
                }
                // only MODIFICATOR+KEY
                if (cooldownEntry.Value.Key == key && cooldownEntry.Value.Modifiers == modifiers)
                {
                    if (cooldownEntry.Key.buffedCharacter == GameStates.Instance.currentCharacter)
                    {
                        cooldownEntry.Key.renderCooldown.StartRender();
                    }
                }
            }
        }
        public void Pause()
        {
            foreach (var buffEntry in buffsHotkeys)
            {
                //  buffEntry.Key.StopTimer();
            }
            foreach (var cooldownEntry in cooldownHotkeys)
            {
                //cooldownEntry.Key.StopTimer();
            }
        }
        public void Unpause()
        {
            foreach (var buffEntry in buffsHotkeys)
            {
                //buffEntry.Key.StartTimer();
            }
            foreach (var cooldownEntry in cooldownHotkeys)
            {
                //    cooldownEntry.Key.StartTimer();
            }
        }
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
            }
            GameStates.Instance.currentCharacter = hero;
            Debug.WriteLine($"New hero: {hero.name}");
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
            Debug.WriteLine($"Available: {hero.name}");
        }



        #region redirect hotkey



        private void RedirectHotkeys(Key key)
        {

            if (!GameStates.Instance.isWutheringWavesWindow)
            {
                return;
            }
            if (!GameStates.Instance.qolHotkey.isCanselTarget && !GameStates.Instance.qolHotkey.isSwitchTarget)
            {
                return;
            }
            if (true)
            {

            }
            if (GameStates.Instance.qolHotkey?.switchTargetHotkey == "WU")
            {

            }
            var canselTarget = (Key)Enum.Parse(typeof(Key), GameStates.Instance.qolHotkey?.canselTargetHotkey, true);
            var switchTarget = (Key)Enum.Parse(typeof(Key), GameStates.Instance.qolHotkey?.switchTargetHotkey, true);

            if (key == canselTarget && GameStates.Instance.qolHotkey.isCanselTarget)
            {
                CanselTarget();
            }
            else if (key == switchTarget && GameStates.Instance.qolHotkey.isSwitchTarget)
            {
                SendMiddleMouseClick();
            }
        }

        private bool isProcessingMouseWheel = false;

        //private void OnMouseWheel(object sender, MouseEventExtArgs e)
        //{
        //    if (isProcessingMouseWheel)
        //    {
        //        return; // Уже обрабатывается, выходим
        //    }

        //    isProcessingMouseWheel = true;




        //    if (e.Delta < 0) // Прокрутка вниз
        //    {
        //        SendMiddleMouseClick();

        //    }
        //    else if (e.Delta > 0) // Прокрутка вверх
        //    {
        //        CanselTarget();

        //    }

        //    Task.Delay(100).ContinueWith(_ => isProcessingMouseWheel = false);
        //    // Добавляем задержку перед повторной обработкой
        //}

        private bool isCancellingTarget = false;

        public void CanselTarget()
        {
            if (isCancellingTarget)
            {
                return; // Already cancelling, exit
            }

            isCancellingTarget = true;

            // Simulate middle mouse button press
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);

            Task.Delay(350).ContinueWith(_ =>
            {
                // Simulate middle mouse button release
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                isCancellingTarget = false; // Reset the flag
            });
        }
        private void SendMiddleMouseClick()
        {

            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            Task.Delay(25).ContinueWith(_ =>
            {
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            });



            // mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, 0, 0, 0, UIntPtr.Zero);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        #endregion



        #region auto events
        public enum AutoEvent
        {
            RunW,
            RunD,
            RunA,
            RunS,
            Loot
        }
        private Dictionary<AutoEvent, bool> autoEvents = new Dictionary<AutoEvent, bool>();
        private Dictionary<AutoEvent, Task> autoEventsTasks = new();
        private bool isFPressedWithAlt = false;
        private System.Timers.Timer timer;
        private System.Timers.Timer timerLoot;
        private System.Timers.Timer timerW;
        private System.Timers.Timer timerA;
        private System.Timers.Timer timerD;
        public void AutoEvents(Key key, ModifierKeys modifiers)
        {
            if (!GameStates.Instance.isWutheringWavesWindow)
            {
                return;
            }

            if (modifiers == ModifierKeys.Alt && GameStates.Instance.qolHotkey.isAutoRuning)
            {
                if (key == Key.W && !autoEvents.ContainsKey(AutoEvent.RunW) && timerW == null)
                {
                    autoEvents.Add(AutoEvent.RunW, true);
                    StartTimer(ref timerW, Key.W);
                }
                else if (key == Key.D && !autoEvents.ContainsKey(AutoEvent.RunD) && timerD == null)
                {
                    autoEvents.Add(AutoEvent.RunD, true);
                    StartTimer(ref timerD, Key.D);
                }
                else if (key == Key.A && !autoEvents.ContainsKey(AutoEvent.RunA) && timerA == null)
                {
                    autoEvents.Add(AutoEvent.RunA, true);
                    StartTimer(ref timerA, Key.A);
                }

            }

            if (modifiers == ModifierKeys.Alt && key == Key.F && !autoEvents.ContainsKey(AutoEvent.Loot) && timerLoot == null && !isFPressedWithAlt && GameStates.Instance.qolHotkey.isAutoLoot)
            {
                autoEvents.Add(AutoEvent.Loot, true);
                StartTimer(ref timerLoot, Key.F);

                var timer = new System.Timers.Timer();
                timer.Interval = 300; // Временной интервал в миллисекундах
                timer.Elapsed += OnTimerElapsed;
                timer.AutoReset = false; // Выполнить один раз
                timer.Start();

            }
            else if (modifiers == ModifierKeys.Alt && key == Key.F && autoEvents.ContainsKey(AutoEvent.Loot) && isFPressedWithAlt && GameStates.Instance.qolHotkey.isAutoLoot)
            {
                timerLoot?.Stop();
                timerLoot = null;
                autoEvents.Remove(AutoEvent.Loot);
                isFPressedWithAlt = false;
            }

            if (key == Key.S && GameStates.Instance.qolHotkey.isAutoRuning)
            {
                StopTimers();
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            isFPressedWithAlt = true;
        }

        private void StartTimer(ref System.Timers.Timer timer, Key key)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 100; // Временной интервал в миллисекундах

            if (key == Key.F)
            {
                timer.Elapsed += (sender, e) => SendKeyPress(key);
            }
            else
            {
                timer.Elapsed += (sender, e) => SendKeyDown(key);
            }

            timer.AutoReset = true;
            timer.Start();
        }

        private void SendKeyPress(Key key)
        {
            SendKeyDown(key);
            SendKeyUp(key);
        }

        private void SendKeyDown(Key key)
        {
            byte vk = (byte)KeyInterop.VirtualKeyFromKey(key);
            keybd_event(vk, 0, 0, UIntPtr.Zero); // Key down
        }

        private void SendKeyUp(Key key)
        {
            byte vk = (byte)KeyInterop.VirtualKeyFromKey(key);
            keybd_event(vk, 0, 0x0002, UIntPtr.Zero); // Key up
        }

        private void StopTimers()
        {
            SendKeyUp(Key.W);
            SendKeyUp(Key.A);
            SendKeyUp(Key.D);

            timerW?.Stop();
            timerA?.Stop();
            timerD?.Stop();
            timerW = null;
            timerA = null;
            timerD = null;
            autoEvents.Remove(AutoEvent.RunW);
            autoEvents.Remove(AutoEvent.RunD);
            autoEvents.Remove(AutoEvent.RunA);
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        #endregion




    }
}
