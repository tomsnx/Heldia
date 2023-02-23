using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heldia
{
    public class Tree : GameObject
    {
        //Sprite
        SpriteSheets sprite;

        // texture name
        string name = "tree";

        public Tree(int x, int y) : base(x, y, 32, 37, ObjectID.tree)
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
            sprite = new SpriteSheets(g, ObjectID.tree, name);
        }

        public override void Draw(Main g)
        {
            Drawing.FillRect(sprite.GetSheet(), bounds, Color.White, 0, g);
        }
    }
}