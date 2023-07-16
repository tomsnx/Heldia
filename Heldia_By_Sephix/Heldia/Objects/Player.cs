using System.Collections.Generic;
using Heldia.Engine;
using Heldia.Engine.Collisions;
using Heldia.Engine.Enum;
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
    private MouseState _mouse;

    // speeds
    public bool walk;
    public float walkSpeed;
    public float runCoef;
    
    // Direction
    private Vector2 _direction = Vector2.Zero;
    public static bool north;
    public static bool east;
    public static bool south;
    public static bool west;
    
    // Collision
    private Collision _collisionManager;

    // sprite
    private SpriteSheets _sprite;

    // animation
    private Animation _anim;

    // texture name
    private string _name;

    // set dimensions
    public static int playerWidth = 16;
    public static int playerHeight = 32;
    public static int spriteBottomSpace = 27; // Space at the bottom of the sprite

    // Init the started animation
    public static int column = 5; // Started at 0 so n-1 frame
    public static int line = 0; // Started at 0
    
    // Stamina
    public bool StaminaDownToZero { get; set; }
    private bool _lostStamina;
    private bool _isInSprint;

    // Speed
    private float _spd;
    
    // Timers
    private Timer _lifeRegenTimer;
    private Timer _staminaRegenTimer;
    private Timer _staminaLostTimer;

    public Player(int x, int y) : base(x, y, playerWidth, playerHeight, (int)EObjectId.Player)
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

    public override void Init(Main g, List<GameObject> objects)
    {
        // Init anim
        _anim = new Animation();
        
        // Sprite Properties
        _name = "player";

        // Load Sprite
        _sprite = new SpriteSheets(g, (int)EObjectId.Player, _name);
        
        // Collision
        _collisionManager = new Collision();
        _collisionManager.SetMainObj(this);
        _collisionManager.SetObjList(objects);
        _collisionManager.FdelegateX = () =>
        {
            SetSpeed(0, Speed.Y);
        };
        _collisionManager.FdelegateY = () =>
        {
            SetSpeed(Speed.X, 0);
        };
    }

    public override void Update(GameTime gt, Main g)
    {
        // Timers Update
        _lifeRegenTimer.Update(gt);
        _staminaRegenTimer.Update(gt);
        _staminaLostTimer.Update(gt);
        
        _mouse = Mouse.GetState();
        
        // Move
        MovementInput();
        
        // Collision
        _collisionManager.Update();

        // Update x and y positions
        x += Speed.X;
        Instance.PlayerX = GetPositionCentered().X;
        y += Speed.Y;
        Instance.PlayerY = GetPositionCentered().Y;

        // Set and Update the collision rectangle named `bounds`
        SetCollisionBounds(x + (float)width/6, 
                           y + (float)height / 2 + 5, 
                           width - (width/6 * 2), 
                           height / 2 - spriteBottomSpace - 5);

        if (Instance.PlayerStamina == 0)
            StaminaDownToZero = true;

        if (StaminaDownToZero)
        {
            if (Instance.PlayerStamina >= Instance.PlayerMaxStamina)
                StaminaDownToZero = false;
        }

        // Regeneration System
        LifeRegeneration();
        StaminaRegeneration();
        
        // Set variable devideSprite to a X and Y Value of the TileSet
        devideSprite = _anim.GetAnimRect(playerWidth,playerHeight, gt);
    }

    public override void Draw(Main g)
    {
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0.5f, g);
        //Drawing.FillRect(collisionBounds, Color.Red, 0.51f, g);
    }
    
    public override void Destroy(Main g)
    {
        
    }
    
    
    // Other Public Methods
    
    /// <summary>
    /// Decrease life of the player with the dommages parameter
    /// </summary>
    /// <param name="dommage">Dommage that you want to afflige to player</param>
    public void TakeDommage(float dommage)
    {
        if (Instance.PlayerHealth > 0f && 
            (Instance.PlayerHealth - dommage) >= 0f)
        {
            Instance.PlayerHealth -= dommage;
        }
        else
        {
            Instance.PlayerHealth = 0f;
        }
    }

    /// <summary>
    /// Manages all of the player movement.
    /// Check entrys 
    /// </summary>
    private void MovementInput()
    {
        // --- Pressed ---
        CheckSprint();

        if (Instance.Kb.IsKeyDown(Keys.Z)) north = true;
        else north = false;
        
        if (Instance.Kb.IsKeyDown(Keys.S)) south = true;
        else south = false;
        
        if (Instance.Kb.IsKeyDown(Keys.Q)) west = true;
        else west = false;
        
        if (Instance.Kb.IsKeyDown(Keys.D)) east = true;
        else east = false;

        UpdateDirection();

        // Normalize direction vector if necessary
        if (_direction != Vector2.Zero)
        {
            _direction.Normalize();
        }

        SetSpeed(_direction.X * _spd, _direction.Y * _spd);
    }

    /// <summary>
    /// Update the sprite direction and the vector direction
    /// </summary>
    private void UpdateDirection()
    {
        // Diagonal
        if (north && east)
        {
            _direction = new Vector2(1, -1);
            line = (int)ESpriteDirection.East;
        }
        else if (south && east)
        {
            _direction = new Vector2(1, 1);
            line = (int)ESpriteDirection.East;
        }
        else if (north && west)
        {
            _direction = new Vector2(-1, -1);
            line = (int)ESpriteDirection.West;
        }
        else if (south && west)
        {
            _direction = new Vector2(-1, 1);
            line = (int)ESpriteDirection.West;
        }
        // Vertical
        else if (north)
        {
            _direction = new Vector2(0, -1);
            line =(int)ESpriteDirection.North;
        }
        else if (south)
        {
            _direction = new Vector2(0, 1);
            line = (int)ESpriteDirection.South;
        }
        // Horizontal
        else if (east)
        {
            _direction = new Vector2(1, 0);
            line = (int)ESpriteDirection.East;
        }
        else if (west)
        {
            _direction = new Vector2(-1, 0);
            line = (int)ESpriteDirection.West;
        }
        else
        {
            _direction = new Vector2(0, 0);
            line = (int)ESpriteDirection.Idle;
        }
    }

    /// <summary>
    /// Check if the player sprint or not.
    /// </summary>
    private void CheckSprint()
    {
        if (Instance.Kb.IsKeyDown(Keys.LeftShift) && 
            Instance.PlayerStamina >= 0 && 
            !StaminaDownToZero && 
            (xSpeed != 0 || ySpeed != 0))
        {
            _spd = walkSpeed * runCoef * Drawing.delta;
            _staminaLostTimer.Active = true;
            _lostStamina = true;
            _isInSprint = true;
            Instance.sprint = _isInSprint;
        }
        else
        {
            _spd = walkSpeed * Drawing.delta;
            _staminaLostTimer.Active = false;
            _lostStamina = false;
            _isInSprint = false;
            Instance.sprint = _isInSprint;
        }
    }

    /// <summary>
    /// Manages the player's life regeneration by activating
    /// or disable the life timer.
    /// </summary>
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

    /// <summary>
    /// Manages the player's stamina regeneration by
    /// activating or disable the stamina timer.
    /// </summary>
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