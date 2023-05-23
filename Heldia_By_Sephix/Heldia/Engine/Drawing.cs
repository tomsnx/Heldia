using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Engine;
public static class Drawing
{
    // graphics
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;

    // Controls
    private static KeyboardState _kb;

    public static bool IsFullScreen { get; set; } = false;
    
    // window
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static string Title { get; set; } = "Heldia";
    public static bool Vsync { get; set; } = false;

    public static float GoalFps { get; set; } = 144;
    public static int Fps { get; set; }
    
    public static SpriteFont Arial32 { get; set; }
    public static SpriteFont Ubuntu12 { get; set; }
    public static SpriteFont Ubuntu16 { get; set; }
    public static SpriteFont Ubuntu32 { get; set; }
    public static SpriteFont PixExtrusion12 { get; set; }
    public static SpriteFont PixExtrusion16 { get; set; }
    public static SpriteFont PixExtrusion32 { get; set; }

    // dummy tex
    public static Texture2D rect;

    // frametime
    public static float delta, deltaTotal = 0;

    private static int _framerate = 0;

    // init
    public static void Initialize(Main g)
    {
        // set screen size
        Width = 1280;
        Height = 720;

        // init graphics
        graphics.PreferredBackBufferWidth = Width;
        graphics.PreferredBackBufferHeight = Height;
        //graphics.IsFullScreen = true;
        graphics.SynchronizeWithVerticalRetrace = Vsync;
        graphics.ApplyChanges();

        // Load Fonts
        Arial32 = g.Content.Load<SpriteFont>("fonts/Arial32");
        Ubuntu12 = g.Content.Load<SpriteFont>("fonts/Ubuntu12");
        Ubuntu16 = g.Content.Load<SpriteFont>("fonts/Ubuntu16");
        Ubuntu32 = g.Content.Load<SpriteFont>("fonts/Ubuntu32");
        PixExtrusion12 = g.Content.Load<SpriteFont>("fonts/PixExtrusion12");
        PixExtrusion16 = g.Content.Load<SpriteFont>("fonts/PixExtrusion16");
        PixExtrusion32 = g.Content.Load<SpriteFont>("fonts/PixExtrusion32");

        // init
        spriteBatch = new SpriteBatch(g.GraphicsDevice);
    }

    // update
    public static void Update(GameTime gt, Main g)
    {

        changeMode();

        delta = (float)gt.ElapsedGameTime.TotalSeconds;
        _framerate++;
        deltaTotal += delta;

        if (deltaTotal >= 1)
        {
            Fps = (int)(_framerate / deltaTotal);
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

    public static void DrawText(Vector2 position, SpriteFont font, Color textColor, String text, Main g)
    {
        spriteBatch.DrawString(font, text, position, textColor);
    }

    public static void changeMode()
    {
        /*if (_kb.IsKeyDown(Keys.F) && previousKeyboardState.IsKeyUp(Keys.F))
        {
            IsFullScreen = !IsFullScreen;
        }*/
    }
}