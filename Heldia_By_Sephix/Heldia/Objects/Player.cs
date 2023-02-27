using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Heldia;

/// <summary>
/// Player Class which define the properties of the objects.
/// </summary>
public class Player : GameObject
{
    // input
    private KeyboardState _kb;

    // speeds
    public float walkSpeed = 350f;

    // sprite
    private SpriteSheets _sprite;

    // animation
    private Animation _anim;

    // texture name
    private string _name = "blue_fire";

    // set dimensions
    public static int playerWidth = 36;
    public static int playerHeight = 50;

    // init the started animation
    public static int column = 8; // Started to count on column 0
    public static int line = 1; // Started on line 0 but here is useless
    
    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectID.Player)
    {
        
    }

    public override void Init(Main g)
    {
        // init anim
        _anim = new Animation();
    }
    public override void Destroy(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g)
    {
        // input
        _kb = Keyboard.GetState();
        MovementInput(g);

        // move
        x += xSpeed;
        y += ySpeed;

        // Set variable devideSprite to a X and Y Value of the TileSet
        _anim.GetAnimRect(755,1200, gt);
        devideSprite = _anim.rect;

        _sprite = new SpriteSheets(g, ObjectID.Player, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
    }

    private void MovementInput(Main g)
    {
        float spd = walkSpeed * Drawing.delta;

        //pressed
        if (_kb.IsKeyDown(Keys.Z)) { ySpeed = -spd; line = 1; }
        if (_kb.IsKeyDown(Keys.Q)) { xSpeed = -spd; line = 1; }
        if (_kb.IsKeyDown(Keys.S)) { ySpeed = spd; line = 1; }
        if (_kb.IsKeyDown(Keys.D)) { xSpeed = spd; line = 1; }

        //released
        if (_kb.IsKeyUp(Keys.Z) && _kb.IsKeyUp(Keys.S)) { ySpeed = 0; }
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D)) { xSpeed = 0; }

        // set idle animation with line here :
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D)) { line = 1; }
    }
}
