using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia.Engine;
public static class Drawing
{
    // graphics
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;

    // Timer
    private static Timer _globalTimerFps;
    private static Timer _globalTimerTps;

    // window
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static string Title { get; set; }
    public static bool Vsync { get; set; } = false;
    public static int TpsUpdate { get; set; }
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

    private static int _framerateTps = 0;
    private static int _framerateFps = 0;

    // Init
    public static void Initialize(Main g)
    {
        // Title
        Title = "Heldia";
        
        // Set screen size
        Width = 1280;
        Height = 720;
        SetFullScreen(false); // -> false by default but can change it in update
        

        // Init graphics
        graphics.PreferredBackBufferWidth = Width;
        graphics.PreferredBackBufferHeight = Height;
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

        // SpriteBatch
        spriteBatch = new SpriteBatch(g.GraphicsDevice);
        
        // Init Timers
        _globalTimerTps = new Timer(1, () =>
        {
            TpsUpdate = (int)(_framerateTps / _globalTimerTps.TotalProgressSeconds);
            _framerateTps = 0;
        }, true);
        
        _globalTimerFps = new Timer(1, () =>
        {
            FpsDraw = (int)(_framerateFps / _globalTimerFps.TotalProgressSeconds);
            _framerateFps = 0;
        }, true);
    }

    // Update
    public static void Update(GameTime gt, Main g)
    {
        delta = (float)gt.ElapsedGameTime.TotalSeconds;
        
        SetFullScreen(false);

        // Calculate TPS
        _framerateTps++;
        _globalTimerTps.Update(gt);
    }

    public static void Draw(GameTime gt)
    {
        // Calculate FPS
        _framerateFps++;
        _globalTimerFps.Update(gt);
    }

    // Fill Rect
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

    /**
     * Set the mode o the game window
     */
    public static void SetFullScreen(bool isFullScreen)
    {
        // Set the game instance
        Instance.IsFullScreen = isFullScreen;
        
        // Set the game engine
        graphics.IsFullScreen = Instance.IsFullScreen;
        graphics.ApplyChanges();
        
        // Update Width and Height
        if (Instance.IsFullScreen)
        {
            Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
    }
}