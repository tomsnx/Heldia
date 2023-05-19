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
    private MouseState _mouse;

    // speeds
    public float walkSpeed;
    private const float DiagonalDivide = 1.55f; // To have the same speed between x, y, and diagonal speed
    public float runCoef;

    // sprite
    private SpriteSheets _sprite;

    // animation
    private Animation _anim;

    // texture name
    private string _name;

    // set dimensions
    public static int playerWidth = 16;
    public static int playerHeight = 32;
    public int spriteBottomSpace = 25; // Space at the bottom of the sprite

    // init the started animation
    public static int column = 5; // Started at 0 so n-1 frame
    public static int line = 0; // Started at 0
    
    // Life
    public float Life { get; set; }
    public float MaxLife { get; set; }
    private float _coefRegenLife;
    private float _delayRegenLife;
    
    // Stamina
    public float Stamina { get; set; }
    public float MaxStamina { get; set; }
    private float _coefLostStamina;
    private float _coefRegenStamina;
    private float _delayRegenStamina;
    
    // Elapsed Time
    private float _myElapsed;

    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectId.Player)
    {
        // Speed
        walkSpeed = 225f;
        runCoef = 2f;

        // Life
        Life = 100f;
        MaxLife = 100f;
        _coefRegenLife = 1.0f;
        _delayRegenLife = 1000f;
        
        // Stamina
        Stamina  = 100f;
        MaxStamina = 100f;
        _coefLostStamina = 0.25f;
        _coefRegenStamina = 0.5f;
        _delayRegenStamina = 25f;
    }

    public override void Init(Main g)
    {
        // Init anim
        _anim = new Animation();
        
        // Sprite Properties
        _name = "player";
    }
    public override void Destroy(Main g)
    {
        
    }

    public override void Update(GameTime gt, Main g, List<GameObject> objects)
    {
        // input
        _kb = Keyboard.GetState();
        _mouse = Mouse.GetState();
        
        // Set and Update the collision bounds
        SetCollisionBounds(x, y + (float)height / 2, width, height / 2 - spriteBottomSpace);

        // Move
        MovementInput();

        // Check Collisions
        foreach (var obj in objects)
        {
            // TODO: Make an object list to replace this condition
            if (obj == this || obj.id == 1 || obj.id == 2 || obj.id == 10)
            {
                continue;
            }

            if ((xSpeed > 0 && IsTouchingLeft(obj)) || 
                (xSpeed < 0 && IsTouchingRight(obj)))
            {
                xSpeed = 0;
                TakeDommage(0.5f);
            }
            if ((ySpeed > 0 && IsTouchingTop(obj)) || 
                (ySpeed < 0 && IsTouchingBottom(obj)))
            {
                ySpeed = 0;
                TakeDommage(0.5f);
            }
        }

        // Update x and y positions
        x += xSpeed;
        y += ySpeed;

        // Regeneration System
        UpdateLifeAndStamina(gt);
        
        // Set variable devideSprite to a X and Y Value of the TileSet
        _anim.GetAnimRect(playerWidth,playerHeight, gt);
        devideSprite = _anim.rect;
        
        _sprite = new SpriteSheets(g, ObjectId.Player, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 1, g);
        //Drawing.FillRect(collisionBounds, Color.Red, 1, g);
    }
    
    // Other Public Methods
    
    /**
     * Decrease life of the player with the dommages parameter
     */
    public void TakeDommage(float dommage)
    {
        if (Life > 0f && (Life - dommage >= 0f))
        {
            Life -= dommage;
        }
        else
        {
            Life = 0f;
        }
    }
    
    // Private Methods
    private void MovementInput()
    {
        float spd;
        
        // --- Pressed ---
        
        //Sprint
        if (_kb.IsKeyDown(Keys.LeftShift) && Stamina >= 0)
        {
            spd = walkSpeed * runCoef * Drawing.delta;
            Stamina -= _coefLostStamina;
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

    private void UpdateLifeAndStamina(GameTime gt)
    {
        if(Life < MaxLife)
        {
            if(SetupDelay(gt, _delayRegenLife))
            {
                Life += _coefRegenLife;
                _myElapsed = 0;
            }
        }
        
        if(Stamina < MaxStamina && _kb.IsKeyUp(Keys.LeftShift))
        {
            if(SetupDelay(gt, _delayRegenStamina))
            {
                Stamina += _coefRegenStamina;
                _myElapsed = 0;
            }
        }
    }

    /**
     * Set an update with a delay
     */
    private bool SetupDelay(GameTime gt, float delay)
    {
        _myElapsed += (float)gt.ElapsedGameTime.TotalMilliseconds;
        return (_myElapsed >= delay);
    }
}
