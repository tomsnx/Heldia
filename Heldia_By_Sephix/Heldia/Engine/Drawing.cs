using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia.Engine;
public static class Drawing
{
    // graphics
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;

    // window
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static string Title { get; set; } = "Heldia";
    public static bool Vsync { get; set; } = false;
    public static int fps { get; set; }

    // dummy tex
    public static Texture2D rect;

    // frametime
    public static float delta, deltaTotal = 0;

    private static int _framerate = 0;

    // init
    public static void Initialize(Main g)
    {
        // set screen size
        Width = 1080;
        Height = 720;

        // init graphics
        graphics.PreferredBackBufferWidth = Width;
        graphics.PreferredBackBufferHeight = Height;
        graphics.SynchronizeWithVerticalRetrace = Vsync;
        graphics.ApplyChanges();

        // init
        spriteBatch = new SpriteBatch(g.GraphicsDevice);
    }

    // update
    public static void Update(GameTime gt, Main g)
    {
        delta = (float)gt.ElapsedGameTime.TotalSeconds;
        _framerate++;
        deltaTotal += delta;

        if (deltaTotal >= 1)
        {
            fps = (int)(_framerate / deltaTotal);
            _framerate = 0;
            deltaTotal = 0;
        }
    }

    public static void FillRect(Rectangle bounds, Color color, float depth, Main g)
    {
        if(rect == null) { rect = new Texture2D(g.GraphicsDevice, 1, 1); }
        rect.SetData(new[] { Color.White });
        spriteBatch.Draw(rect, bounds, null, color, 0, new Vector2(0, 0), SpriteEffects.None, depth);
    }
    
    public static void FillRect(Rectangle bounds, Color color, float depth, SpriteFont font, Color textColor, String text, Main g)
    {
        if(rect == null) { rect = new Texture2D(g.GraphicsDevice, 1, 1); }
        rect.SetData(new[] { Color.White });
        
        Vector2 textSize = font.MeasureString(text);
        Vector2 textPosition = new Vector2(
            bounds.Center.X - textSize.X / 2,
            bounds.Center.Y - textSize.Y / 2
        );
        
        spriteBatch.Draw(rect, bounds, null, color, 0, new Vector2(0, 0), SpriteEffects.None, depth);
        spriteBatch.DrawString(font, text, textPosition, textColor);
    }

    public static void FillRect(Texture2D texture, Rectangle bounds,Color color, float depth, Main g)
    {
        if (rect == null) { rect = new Texture2D(g.GraphicsDevice, 1, 1); }
        rect.SetData(new[] { Color.White });
        spriteBatch.Draw(texture, bounds, null, color, 0, new Vector2(0, 0), SpriteEffects.None, depth);
    }

    public static void FillRect(Texture2D texture, Rectangle devideSprite, Rectangle bounds, Color color, float depth, Main g)
    {
        if (rect == null) { rect = new Texture2D(g.GraphicsDevice, 1, 1); }
        rect.SetData(new[] { Color.White });
        spriteBatch.Draw(texture, bounds, devideSprite, color, 0, new Vector2(0, 0), SpriteEffects.None, depth);
    }
}