using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Main.Hotkeys
{
    internal class QOL_Hotkeys
    {
        private bool isProcessingMouseWheel = false;
        private bool isCancellingTarget = false;

        public void RedirectHotkeys(Key key)
        {
            if (!GameStates.Instance.isGameWindow)
            {
                return;
            }
            if (!GameStates.Instance.qolHotkey.isCanselTarget && !GameStates.Instance.qolHotkey.isSwitchTarget)
            {
                return;
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

        public void CanselTarget()
        {
            if (isCancellingTarget)
            {
                return;
            }

            isCancellingTarget = true;

            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);

            Task.Delay(350).ContinueWith(_ =>
            {
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                isCancellingTarget = false;
            });
        }

        public void SendMiddleMouseClick()
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            Task.Delay(25).ContinueWith(_ =>
            {
                mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            });
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, nuint dwExtraInfo);

        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;




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
            if (!GameStates.Instance.isGameWindow)
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

            if (modifiers == ModifierKeys.Alt && key == Key.F)
            {
                if (!autoEvents.ContainsKey(AutoEvent.Loot) && timerLoot == null && !isFPressedWithAlt && GameStates.Instance.qolHotkey.isAutoLoot)
                {
                    autoEvents.Add(AutoEvent.Loot, true);
                    StartTimer(ref timerLoot, Key.F);

                    var timer = new System.Timers.Timer();
                    timer.Interval = 300;
                    timer.Elapsed += OnTimerElapsed;
                    timer.AutoReset = false;
                    timer.Start();
                }
                else if (autoEvents.ContainsKey(AutoEvent.Loot) && isFPressedWithAlt && GameStates.Instance.qolHotkey.isAutoLoot)
                {
                    timerLoot?.Stop();
                    timerLoot = null;
                    autoEvents.Remove(AutoEvent.Loot);
                    isFPressedWithAlt = false;
                }
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
            if (!GameStates.Instance.isGameWindow)
            {
                return;
            }
            SendKeyDown(key);
            SendKeyUp(key);
        }

        private void SendKeyDown(Key key)
        {
            byte vk = (byte)KeyInterop.VirtualKeyFromKey(key);
            keybd_event(vk, 0, 0, nuint.Zero); // Key down
        }

        private void SendKeyUp(Key key)
        {
            byte vk = (byte)KeyInterop.VirtualKeyFromKey(key);
            keybd_event(vk, 0, 0x0002, nuint.Zero); // Key up
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
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, nuint dwExtraInfo);
    }
}
