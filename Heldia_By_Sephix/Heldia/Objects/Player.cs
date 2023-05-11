using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Objects;

/// <summary>
/// Player Class which define the properties of the objects.
/// </summary>
public class Player : GameObject
{
    // input
    private KeyboardState _kb;

    // speeds
    public float walkSpeed = 225f;
    public const float WalkSpeedSprint = 700f;
    private const float DiagonalDivide = 1.55f;

    // sprite
    private SpriteSheets _sprite;

    // animation
    private Animation _anim;

    // texture name
    private string _name = "player";

    // set dimensions
    public static int playerWidth = 16;
    public static int playerHeight = 32;

    // init the started animation
    public static int column = 5; // Started at 0 so n-1 frame
    public static int line = 0; // Started at 0

    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectId.Player)
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

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        // input
        _kb = Keyboard.GetState();
        MovementInput();

        // move
        foreach (var obj in objects)
        {
            if (obj == this || obj.id == 2 || obj.id == 1)
            {
                continue;
            }

            if ((this.xSpeed > 0 && this.IsTouchingLeft(obj)) || 
                 (this.xSpeed < 0 && this.IsTouchingRight(obj)))
            {
                this.xSpeed = 0;
            }
            if ((this.ySpeed > 0 && this.IsTouchingTop(obj)) || 
                (this.ySpeed < 0 && this.IsTouchingBottom(obj)))
            {
                this.ySpeed = 0;
            }
        }
        
        x += xSpeed;
        y += ySpeed;
        
        // Set variable devideSprite to a X and Y Value of the TileSet
        _anim.GetAnimRect(playerWidth,playerHeight, gt);
        devideSprite = _anim.rect;
        
        _sprite = new SpriteSheets(g, ObjectId.Player, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0, g);
    }

    private void MovementInput()
    {
        float spd;
        
        // --- Pressed ---
        
        //Sprint
        if (_kb.IsKeyDown(Keys.LeftShift))
        {
            spd = WalkSpeedSprint * Drawing.delta;
        }
        else
        {
            spd = walkSpeed * Drawing.delta;
        }

        //Diagonal movements
        if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.D)) { spd /= DiagonalDivide; }
        if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.Q)) { spd /= DiagonalDivide; }
        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.D)) { spd /= DiagonalDivide; }
        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.Q)) { spd /= DiagonalDivide; }
        
        //Vertical movements
        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.S)) { ySpeed = 0; line = 0; }
        else if (_kb.IsKeyDown(Keys.Z)) { 
            ySpeed = -spd; 
            line = 3;
        }
        else if (_kb.IsKeyDown(Keys.S))
        {
            ySpeed = spd; 
            line = 4;
        }
        
        // Horizontal movements
        if (_kb.IsKeyDown(Keys.Q) && _kb.IsKeyDown(Keys.D)) { xSpeed = 0; line = 0; }
        else if (_kb.IsKeyDown(Keys.Q)) { xSpeed = -spd; line = 2; }
        else if (_kb.IsKeyDown(Keys.D)) { xSpeed = spd; line = 1; }

        // --- Released ---
        if (_kb.IsKeyUp(Keys.Z) && _kb.IsKeyUp(Keys.S)) { ySpeed = 0; }
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D)) { xSpeed = 0; }

        // Set idle animation
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D) &&
            !_kb.IsKeyDown(Keys.Z) && !_kb.IsKeyDown(Keys.S))
        {
            line = 0;
        }
    }
}
