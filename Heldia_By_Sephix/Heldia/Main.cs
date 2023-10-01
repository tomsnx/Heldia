using System;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Menu;
using Heldia.Pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia;

public class Main : Game
{
    // page
    public PageManager pageMgr;
    public PageMenu pageMenu;
    public PageGame pageGame;

    public Main()
    {
        Drawing.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Console.WriteLine("Init");
        Init();

        // init graphics
        Drawing.Initialize(this);

        // Init objects
        pageMgr = new PageManager();
        pageMenu = new PageMenu(pageMgr);
        pageGame = new PageGame(pageMgr);

        // window
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        Window.Title = Instance.GameTitle;

        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        Console.WriteLine("Load Content");
        
        // init pages
        pageMgr.Add(pageMenu, this);
        pageMgr.Add(pageGame, this);
        pageMgr.Set(pageMenu);
        
        // Inputs
        Instance.GameKb = new GameKeyboard();
        
        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        Console.WriteLine("Unload Content");
        
        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // Set the fps mode
        FixedTimeStep(Instance.FixedFps);
        ChangeFpsMode(Instance.GoalFps);
        
        // input
        Instance.GameKb.Update();
        Instance.MouseState = Mouse.GetState();
        UpdateCursor();

        //update pages
        pageMgr.Update(gameTime, this);

        // update drawing
        Drawing.Update(gameTime, this);

        // update base
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        pageMgr.Draw(gameTime, this);

        base.Draw(gameTime);
    }

    /// <summary>
    /// If true, the game will take into account the
    /// TargetElapsedTime(ChangeFpsMode() function) to set the fps to given
    /// number otherwise the game will run at an
    /// unlimited number of FPS.
    /// </summary>
    /// <param name="fixedFps">false as default</param>
    public void FixedTimeStep(bool fixedFps = false)
    {
        IsFixedTimeStep = fixedFps;
    }
    
    /// <summary>
    /// Apply the FPS parameter to the game if FixedTimeStep() is true
    /// </summary>
    /// <param name="fps"></param>
    public void ChangeFpsMode(float fps)
    {
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / fps);
    }

    /// <summary>
    /// Determine which cursor Set on the game
    /// </summary>
    private void UpdateCursor()
    {
        // Arrow Cursor
        if (!Button.IsAnyButtonHovered() &&
            Instance.CurrentMouseCursor != MouseCursor.Arrow)
        {
            Instance.CurrentMouseCursor = MouseCursor.Arrow;
            Mouse.SetCursor(Instance.CurrentMouseCursor);
        }
        // Hand Cursor
        else if (Button.IsAnyButtonHovered() &&
                 Instance.CurrentMouseCursor != MouseCursor.Hand)
        {
            Instance.CurrentMouseCursor = MouseCursor.Hand;
            Mouse.SetCursor(Instance.CurrentMouseCursor);
        }
    }
}