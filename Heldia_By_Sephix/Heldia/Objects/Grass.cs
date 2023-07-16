using System.Collections.Generic;
using System.Linq;
using Heldia.Engine;
using Heldia.Engine.Enum;
using Heldia.Managers;
using Microsoft.Xna.Framework;

namespace Heldia.Objects;

public class Grass : GameObject
{
    // Textures names
    private static readonly string[] GrassNames = {
        "Grass_1",
        "Grass_2",
        "Grass_3",
        "Flower_1",
        "Flower_2",
        "Flower_3"
    };

    public static int stateNumber;
    private int _actualState; // Start at 0 -> 4 grass states in 1 sprite

    // Sprite
    private SpriteSheets _sprite;
    
    // Name of the current object
    private string _name;

    public Grass(int x, int y, int grassState) : base(x, y, Map.tileSize, Map.tileSize, (int)EObjectId.Grass)
    {
        _actualState = grassState;
        devideSprite = new Rectangle(0, 0, width, height);

        _name = GrassNames[_actualState];
        stateNumber = GrassNames.Count();
    }

    public override void Init(Main g, List<GameObject> objects)
    {
        _sprite = new SpriteSheets(g, (int)EObjectId.Grass, _name);
    }

    public override void Destroy(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        if (Active)
        {
            collision = false;
        }
    }

    public override void Draw(Main g)
    {
        if (Active)
            Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
    }
}