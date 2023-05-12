using System;
using System.Collections.Generic;
using System.IO;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia.Pages;

public class PageMenu : Page
{
    // Main
    private Main _g;
    
    // Managers
    private PageManager _pageMgr;
    private ContentManager _content;
    public ObjectManager objMgr;

    // GameObject List (here : buttons...)
    private List<GameObject> _objectsList;

    //Buttons sizes
    private static float _buttonWidth = Drawing.Width * 0.30f;
    private static float _buttonHeight = Drawing.Height * 0.08f;

    //Buttons positions in the screen
    private static  float _middleScreenHeight = (float)Drawing.Height / 2 - _buttonHeight / 2;
    private static  float _middleScreenWidth = (float)Drawing.Width / 2 - _buttonWidth / 2;
    
    private static float _spacingBetween = Drawing.Height * 0.01f;
    private static float _yPositionButtonCenter = _middleScreenHeight + Drawing.Height * 0.15f;

    // Constructor
    public PageMenu(ContentManager content, PageManager pageMgr, Main g) : base(PageId.Menu)
    {
        _content = content;
        _pageMgr = pageMgr;
        objMgr = new ObjectManager();
        
        
    }

    public override void Init(Main g)
    {
        _g = g;
        IsLoad = true;
        
        var newGameButton = new Button((int)_middleScreenWidth, 
            (int)(_yPositionButtonCenter - _buttonHeight - _spacingBetween), 
            (int)_buttonWidth, 
            (int)_buttonHeight,
            999,
            _pageMgr);
        newGameButton.Click += newGamebutton_Click;
        
        var loadGameButton = new Button((int)_middleScreenWidth, 
            (int)_yPositionButtonCenter, 
            (int)_buttonWidth, 
            (int)_buttonHeight, 
            999,
            _pageMgr);
        loadGameButton.Click += loadGamebutton_Click;

        var quitGameButton = new Button((int)_middleScreenWidth, 
            (int)(_yPositionButtonCenter + _buttonHeight + _spacingBetween), 
            (int)_buttonWidth, 
            (int)_buttonHeight, 
            999,
            _pageMgr);
        quitGameButton.Click += quitGamebutton_Click;
        
        _objectsList = new List<GameObject>()
        {
            newGameButton,
            loadGameButton,
            quitGameButton
        };
        
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
        if (IsLoad)
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
    }

    public override void Destroy(Main g)
    {
        IsLoad = false;
        objMgr.Destroy(g);
    }
    
    // Methods - Action on event
    private void newGamebutton_Click(object sender, EventArgs e)
    {
        _pageMgr.ChangePage(new PageGame());
    }
    
    private void loadGamebutton_Click(object sender, EventArgs e)
    {
        Console.WriteLine("Load Game");
    }
    
    private void quitGamebutton_Click(object sender, EventArgs e)
    {
        _g.Exit();
    }
}