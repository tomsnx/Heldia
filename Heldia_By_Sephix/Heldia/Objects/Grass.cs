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
    private string _name;
    private int _grassState = 0; // Start at 0 -> 4 grass states in 1 sprite

    public Grass(int x, int y, int grassState) : base(x, y, Map.tileSize, Map.tileSize, ObjectId.Grass)
    {
        _grassState = grassState;
        devideSprite = new Rectangle(0, 0, width, height);
        
        switch (_grassState)
        { 
            case 1:
                _name = "Grass_2";
                break;
            case 2:
                _name = "Grass_3";
                break;
            case 3:
                _name = "Flower_1";
                break;
            case 4:
                _name = "Flower_2";
                break;
            case 5:
                _name = "Flower_3";
                break;
            default:
                _name = "Grass_1";
                break;
        }
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
        if (Active)
        {
            collision = false;
        }
    }

    public override void Draw(Main g)
    {
        if (Active)
        {
            Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
        }
    }
}