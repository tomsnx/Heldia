using System;
using Heldia.Engine;
using Heldia.Engine.Singleton;
using Heldia.Managers;
using Heldia.Pages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

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
        GameManager.Init();

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
        
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / Drawing.GoalFps);

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
        pageMgr.Draw(this);

        base.Draw(gameTime);
    }
}