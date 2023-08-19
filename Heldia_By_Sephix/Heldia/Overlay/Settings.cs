using System;
using Heldia.Engine;
using Heldia.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Overlay;

public class Settings
{
    public Settings(Player player)
    {
        
    }

    public void Init(Main g)
    {
        
    }

    public void Update(GameTime gt, Main g)
    {
        
    }

    public void Draw(Main g)
    {
        Drawing.FillRect(new Rectangle(50, 50, 
                    Drawing.Width - 100, Drawing.Height - 100), 
                         new Color(Color.Black, 0.648f), 0.999f, g);

        var title = "Settings";
        var textPos = Drawing.Ubuntu32.MeasureString(title);
        Drawing.DrawText(new Vector2((Drawing.Width / (float)2)- textPos.X / 2, 75), Drawing.Ubuntu32, Color.White, title,g);
    }
}