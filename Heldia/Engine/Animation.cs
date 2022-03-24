using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    class Animation
    {
        private Vector2 dims;

        float myElapsed;
        float myDelay = 100;
        int myFrames = 0;

        public Rectangle rect;

        public Animation() { }

        // get
        public void GetAnimRect(int dimsx, int dimsy, GameTime gt)
        {
            this.dims = new Vector2(dimsx, dimsy);

            myElapsed += (float)gt.ElapsedGameTime.TotalMilliseconds;

            if (myElapsed >= myDelay)
            {
                if (myFrames >= Player.column)
                {
                    myFrames = 0;
                }
                else
                {
                    myFrames++;
                }
                myElapsed = 0;
            }
            
            rect = new Rectangle((int)dims.X * myFrames, (int)dims.Y * Player.line, (int)dims.X, (int)dims.Y);
        }
    }
}
