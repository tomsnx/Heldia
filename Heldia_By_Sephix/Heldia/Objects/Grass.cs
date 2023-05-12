using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;

public class Grass : GameObject
{
    //Sprite
    private SpriteSheets _sprite;

    // texture name
    string _name = "grass";
    private int _grassState = 0; // Start at 0 -> 4 grass states in 1 sprite

    public Grass(int x, int y, int grassState) : base(x, y, Map.tileSize, Map.tileSize, ObjectId.Grass)
    {
        _grassState = grassState;
        devideSprite = new Rectangle(width * _grassState, 0, width, height);
    }

    public override void Init(Main g)
    {
        _sprite = new SpriteSheets(g, ObjectId.Grass, _name);
    }

    public override void Destroy(Main g)
    {

    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
    }
}