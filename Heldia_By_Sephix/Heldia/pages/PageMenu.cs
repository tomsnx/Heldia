using System;
using System.Collections.Generic;
using System.Net.Mime;
using Heldia.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia;

public class PageMenu : Page
{
    // Main
    private Main g;
    
    // Managers
    private PageManager pageMgr;
    private ContentManager _content;
    public ObjectManager objMgr;

    // GameObject List (here : buttons...)
    private List<GameObject> _objectsList;
    
    //private SpriteFont ubuntu;
    
    //Buttons sizes
    private static float buttonWidth = Drawing.width * 0.25f;
    private static float buttonHeight = Drawing.height * 0.10f;
    
    //Buttons positions in the screen
    private static  float middleScreenHeight = Drawing.height / 2 - buttonHeight / 2;
    private static  float middleScreenWidth = Drawing.width / 2 - buttonWidth / 2;

    // Constructor
    public PageMenu(ContentManager content, PageManager pageMgr) : base(PageID.menu)
    {
        _content = content;
        this.pageMgr = pageMgr;
        objMgr = new ObjectManager();
        //ubuntu = content.Load<SpriteFont>("Fonts/Ubuntu32");

        var newGameButton = new Button((int)middleScreenWidth, 
                                       (int)(middleScreenHeight - buttonHeight - 50), 
                                       (int)buttonWidth, 
                                       (int)buttonHeight, 
                                       999);
        newGameButton.Click += newGamebutton_Click;
        
        var loadGameButton = new Button((int)middleScreenWidth, 
                                        (int)middleScreenHeight, 
                                        (int)buttonWidth, 
                                        (int)buttonHeight, 
                                        999);
        loadGameButton.Click += loadGamebutton_Click;

        var quitGameButton = new Button((int)middleScreenWidth, 
                                        (int)(middleScreenHeight + buttonHeight + 50), 
                                        (int)buttonWidth, 
                                        (int)buttonHeight, 
                                        999);
        quitGameButton.Click += quitGamebutton_Click;

        _objectsList = new List<GameObject>()
        {
            newGameButton,
            loadGameButton,
            quitGameButton
        };
    }

    public override void Init(Main g)
    {
        this.g = g;
        
        foreach (var obj in _objectsList)
        {
            objMgr.Add(obj, g);
        }
    }

    public override void Update(GameTime gt, Main g)
    {
        objMgr.Update(gt, g);
    }

    public override void Draw(Main g)
    {
        //clear
        g.GraphicsDevice.Clear(Color.BlueViolet);

        // begin
        Drawing.spriteBatch.Begin();

        //objs
        objMgr.Draw(g);

        // end
        Drawing.spriteBatch.End();
    }
    
    // Methods - Action on event
    private void newGamebutton_Click(object sender, EventArgs e)
    {
        pageMgr.ChangePage(new PageGame());
    }
    
    private void loadGamebutton_Click(object sender, EventArgs e)
    {
        Console.WriteLine("Load Game");
    }
    
    private void quitGamebutton_Click(object sender, EventArgs e)
    {
        g.Exit();
    }
}