﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace Wuthering_Waves_comfort_vision.Scripts.Main
{
    internal class DetectCurrentWindow
    {
        public event Action<bool> WindowFocusChanged;

        private Timer timer;
        private bool lastWindowState;

        public DetectCurrentWindow()
        {
            timer = new Timer(1000); // интервал в миллисекундах
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private bool IsWutheringWavesActive()
        {
            const string partialWindowTitle = "Wuthering"; // Используем только часть названия окна для проверки

            IntPtr handle = GetForegroundWindow();
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(handle, sb, sb.Capacity);

            return sb.ToString().Contains(partialWindowTitle);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            bool currentWindowState = IsWutheringWavesActive();

            // Вызываем событие только если состояние окна изменилось
            if (currentWindowState != lastWindowState)
            {
                lastWindowState = currentWindowState;
                WindowFocusChanged?.Invoke(currentWindowState);
            }
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Start()
        {
            timer.Start();
        }
    }
}