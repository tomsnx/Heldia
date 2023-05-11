using System.Collections.Generic;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class Tree : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    private string _name = "tree";

    public Tree(int x, int y) : base(x, y, 32, 37, ObjectId.Tree)
    {

    }

    public override void Init(Main g)
    {

    }
    public override void Destroy(Main g)
    {

    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        _sprite = new SpriteSheets(g, ObjectId.Tree, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
    }
}