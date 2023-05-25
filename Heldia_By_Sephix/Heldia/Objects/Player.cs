using System;
using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;

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
    private bool _lostStamina = false;

    private float _spd;
    
    // Timer
    private Timer _lifeRegenTimer;
    private Timer _staminaRegenTimer;
    private Timer _staminaLostTimer;

    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, ObjectId.Player)
    {
        // Speed
        walkSpeed = 225f;
        runCoef = 2f;

        // Life
        Instance.PlayerHealth = Instance.PlayerMaxHealth;;
        Instance.PlayerCoefRegenHealth = 0.5f;
        Instance.PlayerDelayRegenHealth = 1f;

        // Stamina
        Instance.PlayerStamina = Instance.PlayerMaxStamina;
        Instance.PlayerCoefLostStamina = 1f;
        Instance.PlayerCoefRegenStamina = 0.5f;
        Instance.PlayerDelayRegenStamina = 0.02f;

        
        // Set the timers for health and stamina
        _lifeRegenTimer = new Timer(Instance.PlayerDelayRegenHealth, () =>
        {
            Instance.PlayerHealth += Instance.PlayerCoefRegenHealth;
        }, true, false);

        _staminaRegenTimer = new Timer(Instance.PlayerDelayRegenStamina, () =>
        {
            Instance.PlayerStamina += Instance.PlayerCoefRegenStamina;
        }, true, false);

        _staminaLostTimer = new Timer(0.01f, () =>
        {
            Instance.PlayerStamina -= Instance.PlayerCoefLostStamina;
        }, true, false);
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
        // Timers Update
        _lifeRegenTimer.Update(gt);
        _staminaRegenTimer.Update(gt);
        _staminaLostTimer.Update(gt);

        // input
        _kb = Keyboard.GetState();
        _mouse = Mouse.GetState();
        
        // Move
        MovementInput();

        // Update x and y positions
        x += Speed.X;
        Instance.PlayerX = x;
        y += Speed.Y;
        Instance.PlayerY = y;

        x = 100;
        y = 100;
        
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

        // Check Collisions
        foreach (var obj in objects)
        {
            if (obj.collision)
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
        }

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
        if (Instance.PlayerHealth > 0f && (Instance.PlayerHealth - dommage) >= 0f)
        {
            Instance.PlayerHealth -= dommage;
        }
        else
        {
            Instance.PlayerHealth = 0f;
        }
    }

    // Private Methods
    private void MovementInput()
    {
        // --- Pressed ---
        
        //Sprint
        if (_kb.IsKeyDown(Keys.LeftShift) && Instance.PlayerStamina >= 0 && !StaminaDownToZero)
        {
            _spd = walkSpeed * runCoef * Drawing.delta;
            _staminaLostTimer.Active = true;
            _lostStamina = true;
        }
        else
        {
            _spd = walkSpeed * Drawing.delta;
            _staminaLostTimer.Active = false;
            _lostStamina = false;
        }

        //Diagonal movements
        if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.D))
        {
            SetSpeed(_spd / (float)Math.Sqrt(2), _spd / (float)Math.Sqrt(2));
        }

        if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.Q))
        {
            SetSpeed(-_spd / (float)Math.Sqrt(2), _spd / (float)Math.Sqrt(2));
        }

        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.D))
        {
            SetSpeed(_spd / (float)Math.Sqrt(2), -_spd / (float)Math.Sqrt(2));
        }

        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.Q))
        {
            SetSpeed(-_spd / (float)Math.Sqrt(2), -_spd / (float)Math.Sqrt(2));
        }

        //Vertical movements
        if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.S)) { ySpeed = 0; line = 0; }
        else if (_kb.IsKeyDown(Keys.Z)) { 
            SetSpeed(xSpeed, -_spd);
            line = 3;
        }
        else if (_kb.IsKeyDown(Keys.S))
        {
            SetSpeed(xSpeed, _spd);
            line = 4;
        }
        
        // Horizontal movements
        if (_kb.IsKeyDown(Keys.Q) && _kb.IsKeyDown(Keys.D)) { xSpeed = 0; line = 0; }
        else if (_kb.IsKeyDown(Keys.Q))
        {
            SetSpeed(-_spd, ySpeed);
            line = 2;
        }
        else if (_kb.IsKeyDown(Keys.D))
        {
            SetSpeed(_spd, ySpeed);
            line = 1;
        }

        // --- Released ---
        if (_kb.IsKeyUp(Keys.Z) && _kb.IsKeyUp(Keys.S))
        {
            SetSpeed(xSpeed, 0);
        }

        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D))
        {
            SetSpeed(0, ySpeed);
        }

        // Set idle animation
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D) &&
            !_kb.IsKeyDown(Keys.Z) && !_kb.IsKeyDown(Keys.S))
        {
            line = 0;
        }
    }
    
    private void LifeRegeneration()
    {
        if(Instance.PlayerHealth < Instance.PlayerMaxHealth)
        {
            _lifeRegenTimer.Active = true;
        }
        else
        {
            _lifeRegenTimer.Active = false;
        }
    }

    private void StaminaRegeneration()
    {
        if (_lostStamina) return;
        
        if(Instance.PlayerStamina < Instance.PlayerMaxStamina)
        {
            _staminaRegenTimer.Active = true;
        }
        else
        {
            _staminaRegenTimer.Active = false;
        }
    }
}
