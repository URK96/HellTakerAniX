using System.Timers;

namespace HellTakerAniX.Services;

internal class FrameTimerService
{
    private readonly System.Timers.Timer _timer = new();
    private readonly int _maxCount = 24;
    private int _currentCount = 0;
    
    public EventHandler<int> NextFrameInvoked;

    public FrameTimerService()
    {
        SetFrameInterval(SettingManager.Instance.Setting.FrameSpeed);

        _timer.Elapsed += Timer_Elapsed;
        _timer.Start();
    }

    public void SetFrameInterval(TimeSpan intervalTime) =>
        _timer.Interval = intervalTime.TotalMilliseconds;

    public void SetFrameInterval(int framePerSecond) =>
        SetFrameInterval(TimeSpan.FromSeconds(1 / (double)framePerSecond));

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        NextFrameInvoked?.Invoke(sender, _currentCount);
        IncreaseCurrentFrameCount();
    }

    private void IncreaseCurrentFrameCount()
    {
        _currentCount += 1;

        if (_currentCount >= _maxCount)
        {
            _currentCount = 0;
        }
    }
}