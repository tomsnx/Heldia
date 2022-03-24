#region
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Heldia
{
    public class Main : Game
    {
        public static ContentManager content;

        // page
        public PageManager pageMgr = new PageManager();
        public PageGame pageGame = new PageGame();

        public Main()
        {
            Drawing.graphics = new GraphicsDeviceManager(this);

            // content Dir
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // init graphics
            Drawing.Initialize(this);

            // window
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            Window.Title = Drawing.title;

            // init pages
            pageMgr.Add(pageGame, this);
            pageMgr.Set(pageGame);
            base.Initialize();
        }

        // Loading Content Method
        protected override void LoadContent()
        {
            
        }

        protected override void UnloadContent()
        {

        }

        // Updating Method
        protected override void Update(GameTime gameTime)
        {
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
}
