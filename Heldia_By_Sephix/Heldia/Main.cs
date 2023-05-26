using System;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static Heldia.Engine.Singleton.GameManager;

namespace Heldia;

public class Main : Game
{
    public static ContentManager content;

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
        pageMenu = new PageMenu(content, pageMgr, this);
        pageGame = new PageGame();

        // window
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        Window.Title = Drawing.Title;

        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        Console.WriteLine("Load Content");
        
        // init pages
        pageMgr.Add(pageMenu, this);
        pageMgr.Add(pageGame, this);
        pageMgr.Set(pageMenu);
        
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
        FixedTimeStep(true);
        ChangeFpsMode(Instance.GoalFps);
        
        //update pages
        pageMgr.Update(gameTime, this);

        // update drawing
        Drawing.Update(gameTime, this);
        Window.Title = "Heldia";

        // update base
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        pageMgr.Draw(gameTime, this);

        base.Draw(gameTime);
    }

    public void FixedTimeStep(bool fixedFps = false)
    {
        IsFixedTimeStep = fixedFps;
    }
    public void ChangeFpsMode(float fps)
    {
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / fps);
    }
}