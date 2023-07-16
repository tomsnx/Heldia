using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class Block : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    private string _name = "carreRouge";

    public Block(int x, int y) : base(x, y, 48, 48, (int)EObjectId.Block)
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
        _sprite = new SpriteSheets(g, (int)EObjectId.Block, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect( _sprite.GetSheet(),bounds, Color.Red, 0, g);
    }
}