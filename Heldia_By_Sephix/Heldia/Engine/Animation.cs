﻿using Heldia.Objects;
using Microsoft.Xna.Framework;

namespace Heldia.Engine;
class Animation
{
    public Rectangle rect;
    private Vector2 _dims;
    
    private float _myDelay = 0.1f; // Seconds
    private int _myFrames = 0;

    private Timer _timer;

    // Constructor
    public Animation()
    {
        _timer = new Timer(_myDelay, () =>
        {
            if (_myFrames >= Player.column)
            {
                _myFrames = 0;
            }
            else
            {
                _myFrames++;
            }
        }, true);
    }

    // Get
    public void GetAnimRect(int dimsx, int dimsy, GameTime gt)
    {
        _dims = new Vector2(dimsx, dimsy);
        
        _timer.Update(gt);
        
        rect = new Rectangle((int)_dims.X * _myFrames, (int)_dims.Y * Player.line, (int)_dims.X, (int)_dims.Y);
    }
}
