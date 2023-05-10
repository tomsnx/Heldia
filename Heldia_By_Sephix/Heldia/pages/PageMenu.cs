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
    private PageManager pageMgr;
    public ObjectManager objMgr;
    private List<GameObject> _objectsList;

    private ContentManager _content;
    //private SpriteFont ubuntu;

    public PageMenu(ContentManager content, PageManager pageMgr) : base(PageID.menu)
    {
        _content = content;
        this.pageMgr = pageMgr;
        objMgr = new ObjectManager();
        //ubuntu = content.Load<SpriteFont>("Fonts/Ubuntu32");

        var newGameButton = new Button(0, 0, 300, 100, 999);
        newGameButton.Click += newGamebutton_Click;
        
        var loadGameButton = new Button(0, 150, 300, 100, 999);
        loadGameButton.Click += loadGamebutton_Click;

        var quitGameButton = new Button(0, 300, 300, 100, 999);
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
        Console.WriteLine("Quit game");
    }
}