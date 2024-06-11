using System;
using System.Timers;
using System.Windows.Input;

namespace Wuthering_Waves_comfort_vision.Scripts.Sub
{
    internal class TimemInput
    {
        private Key key;
        private double duration;
        private DateTime lastPressTime;
        private Timer timer;
        private bool isWithinTimeFrame;

        public TimemInput(Key key, double duration)
        {
            this.key = key;
            this.duration = duration * 1000; // Convert duration to milliseconds
            this.lastPressTime = DateTime.MinValue;
            this.isWithinTimeFrame = false;

            timer = new Timer(this.duration);
            timer.Elapsed += TimerElapsed;
        }

        public bool RegisterKeyPress(Key key)
        {
            if (this.key == key)
            {
                DateTime currentTime = DateTime.Now;
                if ((currentTime - lastPressTime).TotalMilliseconds < duration)
                {
                    // Второе нажатие в пределах временного интервала
                    timer.Stop(); // Останавливаем таймер, так как второе нажатие произошло
                    isWithinTimeFrame = true;
                }
                else
                {
                    // Первое нажатие или второе нажатие вне временного интервала
                    lastPressTime = currentTime;
                    isWithinTimeFrame = false;
                    timer.Start(); // Запускаем таймер для отслеживания интервала времени
                }
            }
            return isWithinTimeFrame;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Время истекло, сбрасываем флаг
            isWithinTimeFrame = false;
            timer.Stop();
        }
    }
}