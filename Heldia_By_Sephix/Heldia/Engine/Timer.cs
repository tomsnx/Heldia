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

    public Function fdelegate { get; set; }

    public Timer(double totalSeconds, Function codeToExecute = null, bool autoReset = false, bool active = true)
    {
        TotalSeconds = totalSeconds;
        this.autoReset = autoReset;
        fdelegate = codeToExecute;
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

            if (TotalProgressSeconds >= TotalSeconds && fdelegate != null)
            {
                fdelegate();
                end = true;
            }

            if (autoReset && end)
            {
                Reset();
            }
        }
    }
}