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
        Drawing.FillRect(new Rectangle(100, 100, Drawing.Width - 200, Drawing.Height - 200), Color.Red, 0.9f, g);
    }
}