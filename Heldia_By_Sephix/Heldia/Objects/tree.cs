using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class Tree : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    private string _name = "tree";

    public Tree(int x, int y) : base(x, y, 32, 37, (int)EObjectId.Tree)
    {

    }

    public override void Init(Main g, List<GameObject> objects)
    {

    }
    public override void Destroy(Main g)
    {

    }

    public override void Update(GameTime gt, Main g)
    {
        _sprite = new SpriteSheets(g, (int)EObjectId.Tree, _name);
    }

    public override void Draw(GameTime gt, Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
    }
}