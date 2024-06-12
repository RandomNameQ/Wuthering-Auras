using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;
using Wuthering_Waves_comfort_vision.Xaml.Main.OverlayImages;
public class Ability
{
    [NonSerialized]
    public ImageSource image;
    public string path;
    public bool isCanRenderImage;
    [NonSerialized]
    public bool isImageNowRender;
    public bool isGlobalBuff;
    public bool isBuffForNextCharacter;
    public bool isCancelWhenSwapCharacter;
    public bool isStartSoundAlert;
    public bool isEndSoundAlert;
    public bool isCanselIfDoubleTap;
    public bool isShowCooldown = true;
    public bool isReusedIfSpam = true;
    private bool isCooldownImage;
    public float duration = 10;
    public string hotKeyToActivate = "Q";
    public string hotKeyDoubleTap = "Q";
    public int width;
    public int height;
    public OverlayImageData overlayImageData = new();
    [NonSerialized]
    private System.Timers.Timer timer;
    [NonSerialized]
    private DateTime startTime;
    [NonSerialized]
    private OverlayImage imageWindow;
    [NonSerialized]
    public Character buffedCharacter;
    public double elapsedTime;
    [NonSerialized]
    public RenderCooldown renderCooldown;
    [NonSerialized]
    public RenderBuffs renderBuffs;
    [NonSerialized]
    public System.Timers.Timer spamTimer;
    [NonSerialized]
    public double spamTimerDuration = 1;
    [NonSerialized]
    public bool isSpamTimerOver = false;
    //сделать чтобы когнда отображаю бафы можно было сохарнять позици и размер в рендере
    [NonSerialized]
    public bool isImageRanderForBuffOverlay;



    public class RenderBuffs
    {
        public Ability ability;
        public RenderBuffs(Ability ability)
        {
            this.ability = ability;
        }
        public void StartRender()
        {

            if (ability.isImageNowRender && !ability.isReusedIfSpam) return;
            if (ability.isSpamTimerOver) return;
            StartSpamTimer();
            CreateImage();
            SetTimer();
        }
        public void StopRender()
        {
            ability.SaveData();

            ability.timer.Stop();
            ability.timer = null;
            ability.spamTimer?.Stop();
            ability.spamTimer = null;
            ability.imageWindow?.Close();
            ability.imageWindow = null;
            ability.isImageNowRender = false;
            ability.isSpamTimerOver = false;
        }
        private void CreateImage()
        {
            if (ability.imageWindow != null) return;
            ability.imageWindow = Helper.CreateImageWindow(
                ability.overlayImageData.height,
                ability.overlayImageData.width,
                ability.overlayImageData.x,
                ability.overlayImageData.y
            );
            ability.imageWindow.cooldownTimerTextBlock.FontSize = (int)ability.overlayImageData.width / 3;
            ability.imageWindow.UpdateImage(ability.path, ability);
        }
        private void StartSpamTimer()
        {
            if (ability.spamTimer != null)
            {
                ability.spamTimer.Stop();
                ability.spamTimer.Dispose();
            }
            ability.isSpamTimerOver = false;
            ability.spamTimer = new System.Timers.Timer(ability.spamTimerDuration * 1000); // duration in milliseconds
            ability.spamTimer.Elapsed += OnSpamTimerElapsed;
            ability.spamTimer.Start();
        }
        private void OnSpamTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ability.isSpamTimerOver = true;
            ability.spamTimer.Stop();
            ability.spamTimer.Dispose();
        }
        private void SetTimer()
        {
            ability.elapsedTime = 0;
            ability.startTime = DateTime.Now;
            ability.timer = new System.Timers.Timer(100); // 100 milliseconds interval
            ability.timer.Elapsed += OnTimerTick;
            ability.timer.Start();
            ability.imageWindow.cooldownTimerTextBlock.Text = ability.duration.ToString("F1");
            ability.isImageNowRender = true;
        }
        public async Task PauseForDurationAsync(float duration)
        {
            if (ability.timer == null) return;
            ability.imageWindow.Hide();
            ability.timer.Stop();
            await Task.Delay(TimeSpan.FromSeconds(duration));
            ability.imageWindow.Show();
            ability.timer.Start();
        }
        public void StopTimer()
        {
            if (ability.timer == null) return;
            ability.isImageNowRender = false;
            ability.imageWindow.Hide();
            ability.timer.Stop();
        }
        public void StartTimer()
        {
            if (ability.timer == null) return;
            ability.imageWindow.Show();
            ability.timer.Start();
        }
        public void ResumeTimer()
        {
            ability.timer?.Start();
        }
        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            ability.elapsedTime = (DateTime.Now - ability.startTime).TotalSeconds;

            // Check if the Dispatcher can access the UI
            if (ability.imageWindow.Dispatcher.CheckAccess())
            {
                try
                {
                    if (ability.imageWindow == null) return;

                    // Защита если юзер не указал длительность
                    if (ability.duration == 0) ability.duration = 1;
                    UpdateOverlayHeight();
                    UpdateCooldownText();

                    if (ability.elapsedTime >= ability.duration)
                    {
                        EndCooldown();
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle the task cancellation exception
                    // Log or handle the exception as needed
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions that may occur
                    // Log or handle the exception as needed
                }
            }
            else
            {
                // If the Dispatcher cannot access the UI, invoke it
                ability.imageWindow.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        if (ability.imageWindow == null) return;

                        // Защита если юзер не указал длительность
                        if (ability.duration == 0) ability.duration = 1;
                        UpdateOverlayHeight();
                        UpdateCooldownText();

                        if (ability.elapsedTime >= ability.duration)
                        {
                            EndCooldown();
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Handle the task cancellation exception
                        // Log or handle the exception as needed
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions that may occur
                        // Log or handle the exception as needed
                    }
                });
            }
        }

        private void UpdateOverlayHeight()
        {
            double remainingTime = ability.duration - ability.elapsedTime;
            double percentage = remainingTime / ability.duration;
            double newHeight = Math.Max(0, ability.imageWindow.ActualHeight * percentage);
            ability.imageWindow.darkOverlay.Height = (ability.isCooldownImage && newHeight <= 0) ? 0 : newHeight;
        }
        private void UpdateCooldownText()
        {
            double remainingTime = ability.duration - ability.elapsedTime;
            ability.imageWindow.cooldownTimerTextBlock.Text = remainingTime.ToString("F1");
        }
        private void EndCooldown()
        {
            ability.timer.Stop();
            if (ability.imageWindow != null)
            {
                ability.isSpamTimerOver = false;
                ability.isImageNowRender = false;
                ability.imageWindow.cooldownTimerTextBlock.Text = "";
                ability.imageWindow.darkOverlay.Height = ability.imageWindow.ActualHeight;
            }
        }
    }
    public class RenderCooldown
    {
        public Ability ability;
        public RenderCooldown(Ability ability)
        {
            this.ability = ability;
        }
        public void StartRender()
        {
            if (ability.isImageNowRender && !ability.isReusedIfSpam) return;
            if (ability.isSpamTimerOver) return;
            StartSpamTimer();
            CreateImage();
            SetTimer();
        }
        public void StopRender()
        {
            ability.SaveData();

            ability.timer.Stop();
            ability.timer = null;
            ability.spamTimer?.Stop();
            ability.spamTimer = null;
            ability.imageWindow.Close();
            ability.imageWindow = null;
            ability.isImageNowRender = false;
            ability.isSpamTimerOver = false;
        }
        private void CreateImage()
        {
            if (ability.imageWindow != null) return;
            ability.imageWindow = Helper.CreateImageWindow(
                ability.overlayImageData.height,
                ability.overlayImageData.width,
                ability.overlayImageData.x,
                ability.overlayImageData.y
            );
            ability.imageWindow.darkOverlay.Height = 100;
            ability.imageWindow.cooldownTimerTextBlock.FontSize = (int)ability.overlayImageData.width / 3;
            ability.imageWindow.UpdateImage(ability.path, ability);
        }
        private void StartSpamTimer()
        {
            if (ability.spamTimer != null)
            {
                ability.spamTimer.Stop();
                ability.spamTimer.Dispose();
            }
            ability.isSpamTimerOver = false;
            ability.spamTimer = new System.Timers.Timer(ability.spamTimerDuration * 1000); // duration in milliseconds
            ability.spamTimer.Elapsed += OnSpamTimerElapsed;
            ability.spamTimer.Start();
        }
        private void OnSpamTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ability.isSpamTimerOver = true;
            ability.spamTimer.Stop();
            ability.spamTimer.Dispose();
        }
        private void SetTimer()
        {
            ability.elapsedTime = 0;
            ability.startTime = DateTime.Now;
            ability.timer = new System.Timers.Timer(100); // 100 milliseconds interval
            ability.timer.Elapsed += OnTimerTick;
            ability.timer.Start();
            ability.imageWindow.cooldownTimerTextBlock.Text = ability.duration.ToString("F1");
            ability.isImageNowRender = true;
        }
        public async Task PauseForDurationAsync(float duration)
        {
            if (ability.timer == null) return;
            ability.imageWindow.Hide();
            ability.timer.Stop();
            await Task.Delay(TimeSpan.FromSeconds(duration));
            ability.imageWindow.Show();
            ability.timer.Start();
        }
        public void StopTimer()
        {
            if (ability.timer == null) return;
            ability.isImageNowRender = false;
            ability.imageWindow.Hide();
            ability.timer.Stop();
        }
        public void StartTimer()
        {
            if (ability.timer == null) return;
            ability.imageWindow.Show();
            ability.timer.Start();
        }
        public void ResumeTimer()
        {
            ability.timer?.Start();
        }
        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            ability.elapsedTime = (DateTime.Now - ability.startTime).TotalSeconds;

            // Check if the Dispatcher can access the UI
            if (ability.imageWindow.Dispatcher.CheckAccess())
            {
                try
                {
                    if (ability.imageWindow == null) return;

                    // Защита если юзер не указал длительность
                    if (ability.duration == 0) ability.duration = 1;
                    UpdateOverlayHeight();
                    UpdateCooldownText();

                    if (ability.elapsedTime >= ability.duration)
                    {
                        EndCooldown();
                    }
                }
                catch (TaskCanceledException)
                {
                    // Handle the task cancellation exception
                    // Log or handle the exception as needed
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions that may occur
                    // Log or handle the exception as needed
                }
            }
            else
            {
                // If the Dispatcher cannot access the UI, invoke it
                ability.imageWindow.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        if (ability.imageWindow == null) return;

                        // Защита если юзер не указал длительность
                        if (ability.duration == 0) ability.duration = 1;
                        UpdateOverlayHeight();
                        UpdateCooldownText();

                        if (ability.elapsedTime >= ability.duration)
                        {
                            EndCooldown();
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        // Handle the task cancellation exception
                        // Log or handle the exception as needed
                    }
                    catch (Exception ex)
                    {
                        // Handle any other exceptions that may occur
                        // Log or handle the exception as needed
                    }
                });
            }
        }

        private void UpdateOverlayHeight()
        {
            double remainingTime = ability.duration - ability.elapsedTime;
            double percentage = remainingTime / ability.duration;
            double newHeight = Math.Max(0, ability.imageWindow.ActualHeight * percentage);
            ability.imageWindow.darkOverlay.Height = (ability.isCooldownImage && newHeight <= 0) ? 0 : newHeight;
        }
        private void UpdateCooldownText()
        {
            double remainingTime = ability.duration - ability.elapsedTime;
            ability.imageWindow.cooldownTimerTextBlock.Text = remainingTime.ToString("F1");
        }
        private void EndCooldown()
        {
            ability.timer.Stop();
            if (ability.imageWindow != null)
            {
                ability.isSpamTimerOver = false;
                ability.isImageNowRender = false;
                ability.imageWindow.cooldownTimerTextBlock.Text = "";
            }
        }
    }
    public Ability()
    {
        renderCooldown = new(this);
        renderBuffs = new(this);
        Subscribe();
    }
    ~Ability()
    {
        Unsubscribe();
    }

    public void Subscribe()
    {
        GlobalEvents.ChangeRenderState += OnChangeRenderState;
        GlobalEvents.RenderImages += RenderImage;
        GlobalEvents.UnRenderImages += UnRenderImage;
        GlobalEvents.SwitchMoveImagePosibility += CanChangeSizePositionIcon;
    }
    public void Unsubscribe()
    {
        GlobalEvents.ChangeRenderState -= OnChangeRenderState;
        GlobalEvents.RenderImages -= RenderImage;
        GlobalEvents.UnRenderImages -= UnRenderImage;
        GlobalEvents.SwitchMoveImagePosibility -= CanChangeSizePositionIcon;

    }
    public void CanChangeSizePositionIcon(bool canChange)
    {
        if (canChange)
        {
            overlayImageData.overlayImage?.Subscribe();
        }
        else
        {
            overlayImageData.overlayImage?.UnSubscribe();
        }
    }
    public void SaveData()
    {
        if (imageWindow == null)
        {
            return;
        }
        overlayImageData.x = (int)imageWindow.Left;
        overlayImageData.y = (int)imageWindow.Top;
        //overlayImageData.width = (int)imageWindow.Width;
        //overlayImageData.height = (int)imageWindow.Height;
        //width = overlayImageData.width;
        //height = overlayImageData.height;
    }

    public void ResetData()
    {

        image = null;
        path = string.Empty;
        isCanRenderImage = false;
        isImageNowRender = false;
        isGlobalBuff = false;
        isBuffForNextCharacter = false;
        isCancelWhenSwapCharacter = false;
        isStartSoundAlert = false;
        isEndSoundAlert = false;
        isCanselIfDoubleTap = false;
        isShowCooldown = true;
        isReusedIfSpam = true;
        isCooldownImage = false;
        duration = 10;
        hotKeyToActivate = "Q";
        hotKeyDoubleTap = "Q";
        width = 0;
        height = 0;
        overlayImageData = new OverlayImageData();
        timer = null;
        startTime = default(DateTime);
        imageWindow = null;
        buffedCharacter = null;
        elapsedTime = 0;
        renderCooldown = null;
        renderBuffs = null;
        spamTimer = null;
        spamTimerDuration = 1;
        isSpamTimerOver = false;
        isImageRanderForBuffOverlay = false;

        return;
        // приводим все поля к дефолтным значениям
        var type = typeof(Ability);
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (field.IsNotSerialized) continue;

            object defaultValue = field.FieldType.IsValueType ? Activator.CreateInstance(field.FieldType) : null;
            field.SetValue(this, defaultValue);
        }
    }


    private void OnChangeRenderState(bool isRenderNow)
    {
        if (imageWindow == null) return;
        if (isRenderNow)
        {
            UnRenderImage();
        }
        else
        {
            RenderImage();
        }
    }
    public void RenderImage() => imageWindow?.Show();
    public void UnRenderImage() => imageWindow?.Hide();



}
