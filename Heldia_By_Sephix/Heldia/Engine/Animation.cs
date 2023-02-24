using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia;
class Animation
{
    private Vector2 _dims;

    private float _myElapsed;
    private float _myDelay = 100;
    private int _myFrames = 0;

    public Rectangle rect;

    public Animation() { }

    // get
    public void GetAnimRect(int dimsx, int dimsy, GameTime gt)
    {
        _dims = new Vector2(dimsx, dimsy);

        _myElapsed += (float)gt.ElapsedGameTime.TotalMilliseconds;

        if (_myElapsed >= _myDelay)
        {
            if (_myFrames >= Player.column)
            {
                _myFrames = 0;
            }
            else
            {
                _myFrames++;
            }
            _myElapsed = 0;
        }
        
        rect = new Rectangle((int)_dims.X * _myFrames, (int)_dims.Y * Player.line, (int)_dims.X, (int)_dims.Y);
    }
}
