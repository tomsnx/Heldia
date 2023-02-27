using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class Tree : GameObject
    {
        //Sprite
        private SpriteSheets _sprite;

        // texture name
        private string _name = "tree";

        public Tree(int x, int y) : base(x, y, 32, 37, ObjectID.Tree)
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
            _sprite = new SpriteSheets(g, ObjectID.Tree, _name);
        }

        public override void Draw(Main g)
        {
            Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
        }
    }
}