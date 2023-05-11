using System.Collections.Generic;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;
public class Block : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    private string _name = "carreRouge";

    public Block(int x, int y) : base(x, y, 48, 48, ObjectId.Block)
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
        _sprite = new SpriteSheets(g, ObjectId.Block, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect( _sprite.GetSheet(),bounds, Color.Red, 0, g);
    }
}