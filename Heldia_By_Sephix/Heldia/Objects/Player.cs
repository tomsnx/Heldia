using System;
using System.Collections.Generic;
using System.Numerics;
using Heldia.Engine;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;
using Vector2 = Microsoft.Xna.Framework.Vector2;

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
    private bool _isInSprint = false;

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
        Instance.PlayerCoefRegenHealth = 1f;
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
        MovementInput(objects);

        // Update x and y positions
        x += Speed.X;
        Instance.PlayerX = x;
        y += Speed.Y;
        Instance.PlayerY = y;

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
    private void MovementInput(List<GameObject> objects)
    {
        // --- Pressed ---

        // Sprint
        if (_kb.IsKeyDown(Keys.LeftShift) && Instance.PlayerStamina >= 0 && !StaminaDownToZero)
        {
            _spd = walkSpeed * runCoef * Drawing.delta;
            _staminaLostTimer.Active = true;
            _lostStamina = true;
            _isInSprint = true;
        }
        else
        {
            _spd = walkSpeed * Drawing.delta;
            _staminaLostTimer.Active = false;
            _lostStamina = false;
            _isInSprint = false;
        }

        Vector2 direction = Vector2.Zero;

        //Diagonal movements
        if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.D))
        {
            direction = new Vector2(1, 1);
            line = 1;
        }
        else if (_kb.IsKeyDown(Keys.S) && _kb.IsKeyDown(Keys.Q))
        {
            direction = new Vector2(-1, 1);
            line = 2;
        }
        else if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.D))
        {
            direction = new Vector2(1, -1);
            line = 1;
        }
        else if (_kb.IsKeyDown(Keys.Z) && _kb.IsKeyDown(Keys.Q))
        {
            direction = new Vector2(-1, -1);
            line = 2;
        }
        
        //Vertical movements
        else if (_kb.IsKeyDown(Keys.Z))
        {
            direction = new Vector2(0, -1);
            line = 3;
        }
        else if (_kb.IsKeyDown(Keys.S))
        {
            direction = new Vector2(0, 1);
            line = 4;
        }
        
        // Horizontal movements
        else if (_kb.IsKeyDown(Keys.Q))
        {
            direction = new Vector2(-1, 0);
            line = 2;
        }
        else if (_kb.IsKeyDown(Keys.D))
        {
            direction = new Vector2(1, 0);
            line = 1;
        }

        // Normalize direction vector if necessary
        if (direction != Vector2.Zero)
        {
            direction.Normalize();
        }

        SetSpeed(direction.X * _spd, direction.Y * _spd);
        
        // TODO: Faire un objet collision avec 2 délégates pour lui
        // TODO: passer le code à faire en cas de collision sur x ET y
        // Check Collisions
        foreach (var obj in objects)
        {
            if (obj.collision)
            {
                if(obj.id == 1)
                {
                    if ((Speed.X > 0 && IsTouchingLeft(obj)) || 
                        (Speed.X < 0 && IsTouchingRight(obj)))
                    {
                        SetSpeed( 0,  Speed.Y);
                    }
                    if ((Speed.Y > 0 && IsTouchingTop(obj)) ||
                        (Speed.Y < 0 && IsTouchingBottom(obj)))
                    {
                        SetSpeed(Speed.X,  0);
                    }
                }
            }
        }

        // --- Released ---
        if (_kb.IsKeyUp(Keys.Z) && _kb.IsKeyUp(Keys.S))
        {
            SetSpeed(Speed.X, 0);
        }

        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D))
        {
            SetSpeed(0, Speed.Y);
        }

        // Set idle animation
        if (_kb.IsKeyUp(Keys.Q) && _kb.IsKeyUp(Keys.D) &&
            _kb.IsKeyUp(Keys.Z) && _kb.IsKeyUp(Keys.S))
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
