using Microsoft.Xna.Framework;

namespace Heldia.Engine;

public delegate void Function();
public class Timer
{
    public bool Active { get; set; }
    private bool end;
    private bool autoReset;
    private bool ignoreSpeed;
    public double TotalSeconds { get; }
    public double TotalProgressSeconds { get; set; }

    public Function Fdelegate { get; set; }

    public Timer(double totalSeconds, Function codeToExecute = null, bool autoReset = false, bool active = true)
    {
        TotalSeconds = totalSeconds;
        this.autoReset = autoReset;
        Fdelegate = codeToExecute;
        Active = active;
    }

    public void Reset()
    {
        TotalProgressSeconds = 0;
        end = false;
    }

    public void Update(GameTime gameTime)
    {
        if (!Active) return;

        if (!end)
        {
            TotalProgressSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (TotalProgressSeconds >= TotalSeconds && Fdelegate != null)
            {
                Fdelegate();
                end = true;
            }

            if (autoReset && end)
            {
                Reset();
            }
        }
    }
}