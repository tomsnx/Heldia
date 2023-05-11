using Heldia.Managers;
using Heldia.Pages;
using Microsoft.Xna.Framework;
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
        // init graphics
        Drawing.Initialize(this);
        
        // Init objects
        pageMgr = new PageManager();
        pageMenu = new PageMenu(content, pageMgr);
        pageGame = new PageGame();

        // window
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        Window.Title = Drawing.title;

        // init pages
        pageMgr.Add(pageMenu, this);
        pageMgr.Add(pageGame, this);
        pageMgr.Set(pageMenu);
        base.Initialize();
    }

    // Loading Content Method
    protected override void LoadContent()
    {

    }
    
    // Loading Content Method
    protected override void UnloadContent()
    {
        
    }

    // Updating Method
    protected override void Update(GameTime gameTime)
    {
        if (pageMgr.nextPage != null)
        {
            pageMgr.Set(pageMgr.nextPage);
            pageMgr.nextPage = null;
        }
        
        //update pages
        pageMgr.Update(gameTime, this);

        // update drawing
        Drawing.Update(gameTime, this);
        Window.Title = "FPS : " + Drawing.fps.ToString();

        // update base
        base.Update(gameTime);
    }

    // Drawing Method
    protected override void Draw(GameTime gameTime)
    {
        pageMgr.Draw(this);

        base.Draw(gameTime);
    }
}