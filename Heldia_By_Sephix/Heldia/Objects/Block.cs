﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class Block : GameObject
    {
        //Sprite
        SpriteSheets sprite;

        // texture name
        string name = "carreRouge";

        public Block(int x, int y) : base(x, y, 48, 48, ObjectID.block)
        {

        }

        public override void Init(Main g)
        {
            
        }
        public override void Destroy(Main g)
        {
            
        }

        public override void Update(GameTime gt, Main g)
        {
            sprite = new SpriteSheets(g, ObjectID.block, name);
        }

        public override void Draw(Main g)
        {
            Drawing.FillRect( sprite.GetSheet(),bounds, Color.Red, 0, g);
        }
    }
}
