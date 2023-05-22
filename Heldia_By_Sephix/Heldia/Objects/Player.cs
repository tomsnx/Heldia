using System;
using System.Collections.Generic;
using System.Threading.Channels;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.GameManager;

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
    public int spriteBottomSpace = 27; // Space at the bottom of the sprite

    // init the started animation
    public static int column = 5; // Started at 0 so n-1 frame
    public static int line = 0; // Started at 0
    public bool StaminaDownToZero { get; set; }

    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectId.Player)
    {
        // Speed
        walkSpeed = 225f;
        runCoef = 2f;

        // Life
        Instance.PlayerLife = 100f;
        Instance.PlayerMaxLife = 100f;
        Instance.PlayerCoefRegenLife = 2f;
        Instance.PlayerDelayRegenLife = 1000f;

        // Stamina
        Instance.PlayerStamina = 100f;
        Instance.PlayerMaxStamina = 100f;
        Instance.PlayerCoefLostStamina = 0.25f;
        Instance.PlayerCoefRegenStamina = 0.5f;
        Instance.PlayerDelayRegenStamina = 25f;
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
        
        // Set and Update the collision rectangle named `bounds`
        SetCollisionBounds(x + (float)width/6, y + (float)height / 2 + 5, width - (width/6 * 2), height / 2 - spriteBottomSpace - 5);

        if (Instance.PlayerStamina == 0)
        {
            StaminaDownToZero = true;
        }

        if (StaminaDownToZero)
        {
            if (Instance.PlayerStamina >= Instance.PlayerMaxStamina)
            {
                StaminaDownToZero = false;
            }
        }
        
        // Move
        MovementInput();

        // Check Collisions
        foreach (var obj in objects)
        {
            if(obj.id == 1)
            {
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
        }

        // Update x and y positions
        x += xSpeed;
        y += ySpeed;

        // Regeneration System
        LifeRegeneration();
        StaminaRegeneration();
        
        // Set variable devideSprite to a X and Y Value of the TileSet
        _anim.GetAnimRect(playerWidth,playerHeight, gt);
        devideSprite = _anim.rect;
        
        _sprite = new SpriteSheets(g, ObjectId.Player, _name);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0.5f, g);
        //Drawing.FillRect(collisionBounds, Color.Red, 0.49f, g);
    }
    
    // Other Public Methods
    
    /**
     * Decrease life of the player with the dommages parameter
     */
    public void TakeDommage(float dommage)
    {
        if (Instance.PlayerLife > 0f && (Instance.PlayerLife - dommage >= 0f))
        {
            Instance.PlayerLife -= dommage;
        }
        else
        {
            Instance.PlayerLife = 0f;
        }
    }
    
    // Private Methods
    private void MovementInput()
    {
        float spd;
        
        // --- Pressed ---
        
        //Sprint
        if (_kb.IsKeyDown(Keys.LeftShift) && Instance.PlayerStamina >= 0 && !StaminaDownToZero)
        {
            spd = walkSpeed * runCoef * Drawing.delta;
            Instance.PlayerStamina -= Instance.PlayerCoefLostStamina;
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

    private void LifeRegeneration()
    {
        if(Instance.PlayerLife < Instance.PlayerMaxLife)
        {
            if(Instance.PlayerRegenLifeClock)
            {
                Instance.PlayerLife += Instance.PlayerCoefRegenLife;
            }
        }
    }

    private void StaminaRegeneration()
    {
        if(Instance.PlayerStamina < Instance.PlayerMaxStamina)
        {
            if(Instance.PlayerRegenStaminaClock)
            {
                Instance.PlayerStamina += Instance.PlayerCoefRegenStamina;
            }
        }
    }
}
