using System;
using System.ComponentModel;
using Heldia.Engine;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Menu;

public class TitleMenu
{
    private String _title;
    public TitleMenu(int x, int y, int charactersWidth, int charactersHeight)
    {
        _title = Instance.GameTitle;
    }

    public void Init(Main g)
    {
        
    }

    public void Update(GameTime gt, Main g)
    {
        
    }

    public void Draw(Main g)
    {
        var textPos = Drawing.Ubuntu32.MeasureString(_title);
        Drawing.DrawText(new Vector2((float)Drawing.Width / 2 - textPos.X / 2, 150), Drawing.Ubuntu32, Color.White, _title,g);
    }
}