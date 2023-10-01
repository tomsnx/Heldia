using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class BaseGrass : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    string _name = "baseGrass";

    public BaseGrass(int x, int y) : base(x, y, Map.tileSize, Map.tileSize, (int)EObjectId.BaseGrass)
    {
        
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        _sprite = new SpriteSheets(g, (int)EObjectId.BaseGrass, _name);
    }
    public override void Destroy(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        if (Active)
        {
            if(collision) collision = false;
        }
    }

    public override void Draw(GameTime gt, Main g)
    {
        if (Active)
        {
            Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
        }
    }
}