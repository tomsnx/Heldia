using System.Collections.Generic;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;

public class Grass01 : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    string _name = "grass01";

    public Grass01(int x, int y) : base(x, y, 64, 64, ObjectId.Grass01)
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
        _sprite = new SpriteSheets(g, ObjectId.Grass01, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), bounds, Color.White, 0, g);
    }
}