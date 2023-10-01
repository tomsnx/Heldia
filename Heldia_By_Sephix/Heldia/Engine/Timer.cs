using Microsoft.Xna.Framework;

namespace Heldia.Engine;

public delegate void Function();
public class Timer
{
    public bool Active { get; set; }
    private bool _end;
    private bool _autoReset;
    private bool _ignoreSpeed;
    public double TotalSeconds { get; }
    public double TotalProgressSeconds { get; set; }

    public Function Fdelegate { get; set; }
    public Function Edelegate { get; set; }

    public Timer(double totalSeconds, Function codeToExecute = null, Function codeToEnd = null, bool autoReset = false, bool active = true)
    {
        TotalSeconds = totalSeconds;
        _autoReset = autoReset;
        Fdelegate = codeToExecute;
        Edelegate = codeToEnd;
        Active = active;
    }

    public void Reset()
    {
        TotalProgressSeconds = 0;
        _end = false;
    }

    public void Update(GameTime gameTime)
    {
        if (!Active) return;

        if (!_end)
        {
            TotalProgressSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (TotalProgressSeconds >= TotalSeconds && Fdelegate != null)
            {
                Fdelegate();
                _end = true;
            }

            if (_autoReset && _end)
            {
                Reset();
            }
        }
        else if (Edelegate != null) // Code which is executed at the end of the timer
        {
            Edelegate();
        }
    }
}