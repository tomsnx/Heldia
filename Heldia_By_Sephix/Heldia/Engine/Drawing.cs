using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public static class Drawing
    {
        // graphics
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        // window
        public static int width { get; private set; }
        public static int height { get; private set; }
        public static string title = "Heldia";
        public static bool vsync { get; private set; } = false;

        // dummy tex
        public static Texture2D rect;

        // frametime
        public static float delta, deltaTotal = 0;
        public static int fps;
        
        private static int framerate = 0;

        // init
        public static void Initialize(Main g)
        {
            // set screen size
            width = 1080;
            height = 720;

            // init graphics
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.SynchronizeWithVerticalRetrace = vsync;
            graphics.ApplyChanges();

            // init
            spriteBatch = new SpriteBatch(g.GraphicsDevice);
        }

        // update
        public static void Update(GameTime gt, Main g)
        {
            delta = (float)gt.ElapsedGameTime.TotalSeconds;
            framerate++;
            deltaTotal += delta;

            if (deltaTotal >= 1)
            {
                fps = (int)(framerate / deltaTotal);
                framerate = 0;
                deltaTotal = 0;
            }
        }

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
    }
}
