using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class BaseGrass : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    string _name = "baseGrass";

    public BaseGrass(int x, int y) : base(x, y, Map.tileSize, Map.tileSize, ObjectId.BaseGrass)
    {
        
    }

    public override void Init(Main g)
    {
        _sprite = new SpriteSheets(g, ObjectId.BaseGrass, _name);
    }
    public override void Destroy(Main g)
    {

    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        if (Active)
        {
            collision = true;
        }
    }

    public override void Draw(Main g)
    {
        if (Active)
        {
            Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
        }
    }
}