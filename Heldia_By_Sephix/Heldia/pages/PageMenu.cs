using System;
using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Heldia.Menu;
using Microsoft.Xna.Framework;

namespace Heldia.Pages;

public class PageMenu : Page
{
    // Main
    private Main _g;
    
    // Managers
    private PageManager _pageMgr;

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

    private TitleMenu _titleMenu;

    // Constructor
    public PageMenu(PageManager pageMgr) : base(PageId.Menu)
    {
        _pageMgr = pageMgr;
        ObjMgr = new ObjectManager();
        _titleMenu = new TitleMenu(Drawing.Width / 2, Drawing.Height / 2, 10, 16);
    }

    public override void Init(Main g)
    {
        _g = g;
        IsLoad = true;
        _titleMenu.Init(g);

        var newGameButton = new Button((int)_middleScreenWidth,
            (int)(_yPositionButtonCenter - _buttonHeight - _spacingBetween),
            (int)_buttonWidth,
            (int)_buttonHeight,
            999,
            "New Game");
        newGameButton.Click += newGamebutton_Click;
        
        var loadGameButton = new Button((int)_middleScreenWidth, 
            (int)_yPositionButtonCenter, 
            (int)_buttonWidth, 
            (int)_buttonHeight, 
            999,
            "Load Game");
        loadGameButton.Click += loadGamebutton_Click;

        var quitGameButton = new Button((int)_middleScreenWidth, 
            (int)(_yPositionButtonCenter + _buttonHeight + _spacingBetween), 
            (int)_buttonWidth, 
            (int)_buttonHeight, 
            999,
            "Exit");
        quitGameButton.Click += quitGamebutton_Click;
        
        _objectsList = new List<GameObject>()
        {
            newGameButton,
            loadGameButton,
            quitGameButton
        };
        
        foreach (var obj in _objectsList)
        {
            ObjMgr.Add(obj, g);
        }
    }

    public override void Update(GameTime gt, Main g)
    {
        ObjMgr.Update(gt, g);
    }

    public override void Draw(GameTime gt, Main g)
    {
        if (IsLoad)
        {
            //clear
            g.GraphicsDevice.Clear(Color.BlueViolet);

            // begin
            Drawing.spriteBatch.Begin();

            //objs
            ObjMgr.Draw(gt, g);
            _titleMenu.Draw(g);

            // end
            Drawing.spriteBatch.End();
        }
    }

    // Methods - Action on event
    private void newGamebutton_Click(object sender, EventArgs e)
    {
        _pageMgr.ChangePage(new PageGame(_pageMgr));
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