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
    
    // Timer 
    private static Timer _globalTimer;

    public static bool IsFullScreen { get; set; } = false;
    
    // window
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static string Title { get; set; } = "Heldia";
    public static bool Vsync { get; set; } = false;

    public static float GoalFps { get; set; } = 144;
    public static int FpsUpdate { get; set; }
    public static int FpsDraw { get; set; }
    
    public static SpriteFont Arial32 { get; set; }
    public static SpriteFont Ubuntu12 { get; set; }
    public static SpriteFont Ubuntu16 { get; set; }
    public static SpriteFont Ubuntu32 { get; set; }
    public static SpriteFont PixExtrusion12 { get; set; }
    public static SpriteFont PixExtrusion16 { get; set; }
    public static SpriteFont PixExtrusion32 { get; set; }

    // Dummy tex
    public static Texture2D rect;

    // Frametime
    public static float delta, deltaTotal = 0, normalizedDelta;

    private static int _framerate = 0;

    // Init
    public static void Initialize(Main g)
    {
        // Set screen size
        Width = 1280;
        Height = 720;

        // Init graphics
        graphics.PreferredBackBufferWidth = Width;
        graphics.PreferredBackBufferHeight = Height;
        //graphics.IsFullScreen = true;
        graphics.SynchronizeWithVerticalRetrace = false;
        graphics.ApplyChanges();

        // Load Fonts
        Arial32 = g.Content.Load<SpriteFont>("fonts/Arial32");
        Ubuntu12 = g.Content.Load<SpriteFont>("fonts/Ubuntu12");
        Ubuntu16 = g.Content.Load<SpriteFont>("fonts/Ubuntu16");
        Ubuntu32 = g.Content.Load<SpriteFont>("fonts/Ubuntu32");
        PixExtrusion12 = g.Content.Load<SpriteFont>("fonts/PixExtrusion12");
        PixExtrusion16 = g.Content.Load<SpriteFont>("fonts/PixExtrusion16");
        PixExtrusion32 = g.Content.Load<SpriteFont>("fonts/PixExtrusion32");

        // Init
        spriteBatch = new SpriteBatch(g.GraphicsDevice);
        
        // Init Timer
        _globalTimer = new Timer(1, () =>
        {
            FpsUpdate = (int)(_framerate / _globalTimer.TotalProgressSeconds);
            _framerate = 0;
        }, true);
    }

    // update
    public static void Update(GameTime gt, Main g)
    {
        delta = (float)gt.ElapsedGameTime.TotalSeconds;

        _framerate++;
        _globalTimer.Update(gt);
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
}