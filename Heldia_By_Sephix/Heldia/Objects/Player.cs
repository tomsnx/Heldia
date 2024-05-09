using System;
using System.Collections.Generic;
using System.Threading;
using Heldia.Engine;
using Heldia.Engine.Collisions;
using Heldia.Engine.Enum;
using Heldia.Engine.Static;
using Heldia.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Heldia.Engine.Singleton.GameManager;
using Timer = Heldia.Engine.Timer;

namespace Heldia.Objects;

/// <summary>
/// Player Class which define the properties of the objects.
/// </summary>
public class Player : GameObject
{
    // speeds
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
    
    public int CurrentSlot { get; private set; }

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
        }, null,true, false);

        _staminaRegenTimer = new Timer(Instance.PlayerDelayRegenStamina, () =>
        {
            Instance.PlayerStamina += Instance.PlayerCoefRegenStamina;
        }, null,true, false);

        _staminaLostTimer = new Timer(0.01f, () =>
        {
            Instance.PlayerStamina -= Instance.PlayerCoefLostStamina;
        }, null,true, false);

        this.CurrentSlot = 0;
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

        // Move
        MovementInput();

        // Update x and y positions
        x += Speed.X;
        y += Speed.Y;

        if (IsMoving())
        {
            // Collisions
            _collisionManager.Update();
            
            Instance.PlayerX = GetPositionCentered().X;
            Instance.PlayerY = GetPositionCentered().Y;
            
            Instance.PlayerTileX = (int)Math.Floor(GetPositionCentered().X / (Instance.TileSize * Instance.MapScale));
            Instance.PlayerTileY = (int)Math.Floor(GetPositionCentered().Y / (Instance.TileSize * Instance.MapScale));
            
            // Set and Update the collision rectangle named `bounds`
            SetCollisionBounds(x + (float)width/6, 
                y + (float)height / 2 + 5, 
                width - (width/6 * 2), 
                height / 2 - spriteBottomSpace - 5);
        }

        if(Instance.GameKb.GetPressedKeys().Length > 0) ItemBarInput();

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
    }

    public override void Draw(GameTime gt, Main g)
    {
        // Set variable devideSprite to a X and Y Value of the TileSet
        devideSprite = _anim.GetAnimRect(playerWidth,playerHeight, gt);
        
        Drawing.FillRect(_sprite.GetSheet(), devideSprite, bounds, Color.White, 0.5f, g);
        //Drawing.FillRect(collisionBounds, Color.Red, 0.51f, g);
    }
    
    public override void Destroy(Main g)
    {
        
    }

    // Other Public Methods

    /// <summary>
    /// Manages all of the player movement.
    /// Check entrys 
    /// </summary>
    private void MovementInput()
    {
        // --- Pressed ---
        CheckSprint();
            
        if (Instance.GameKb.GetKeyPressed(KeysList.top)) north = true;
        else north = false;

        if (Instance.GameKb.GetKeyPressed(KeysList.bottom)) south = true;
        else south = false;

        if (Instance.GameKb.GetKeyPressed(KeysList.left)) west = true;
        else west = false;

        if (Instance.GameKb.GetKeyPressed(KeysList.right)) east = true;
        else east = false;

        UpdateDirection();

        // Normalize direction vector if necessary
        if(IsMoving())
        {
            _direction.Normalize();
        }
        
        SetSpeed(_direction.X * _spd, _direction.Y * _spd);
    }

    /// <summary>
    /// Check if the player is moving with the vector.
    /// </summary>
    /// <returns>True if player move else false.</returns>
    private bool IsMoving()
    {
        return _direction != Vector2.Zero;
    }

    /// <summary>
    /// Update the sprite direction and the vector direction
    /// </summary>
    private void UpdateDirection()
    {
        // Contradictory
        if (north && south || west && east)
        {
            _direction = new Vector2(0, 0);
            line = (int)ESpriteDirection.Idle;
        }
        // Diagonal
        else if (north && east)
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
        if (IsMoving())
        {
            if (Instance.GameKb.GetKeyPressed(KeysList.sprint) &&
                Instance.PlayerStamina >= 0 &&
                !StaminaDownToZero)
            {
                _spd = walkSpeed * runCoef * Drawing.delta;
                _staminaLostTimer.Active = true;
                _lostStamina = true;
                _isInSprint = true;
                Instance.Sprint = _isInSprint;
            }
            else
            {
                _spd = walkSpeed * Drawing.delta;
                _staminaLostTimer.Active = false;
                _lostStamina = false;
                _isInSprint = false;
                Instance.Sprint = _isInSprint;
            }
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

    /// <summary>
    /// Determines which slot is used in the player's inventory bar.
    /// </summary>
    private void ItemBarInput()
    {
        if (Instance.GameKb.KeyboardUsed())
        {
            if (Instance.GameKb.GetKeyPressed(KeysList.one)) CurrentSlot = 0;
            else if (Instance.GameKb.GetKeyPressed(KeysList.two)) CurrentSlot = 1;
            else if (Instance.GameKb.GetKeyPressed(KeysList.three)) CurrentSlot = 2;
            else if (Instance.GameKb.GetKeyPressed(KeysList.four)) CurrentSlot = 3;
            else if (Instance.GameKb.GetKeyPressed(KeysList.five)) CurrentSlot = 4;
            else if (Instance.GameKb.GetKeyPressed(KeysList.six)) CurrentSlot = 5;
            else if (Instance.GameKb.GetKeyPressed(KeysList.seven)) CurrentSlot = 6;
            else if (Instance.GameKb.GetKeyPressed(KeysList.eight)) CurrentSlot = 7;
            else if (Instance.GameKb.GetKeyPressed(KeysList.nine)) CurrentSlot = 8;
            else if (Instance.GameKb.GetKeyPressed(KeysList.zero)) CurrentSlot = 9;
        }
    }
    
    /// <summary>
    /// Decrease life of the player with the dommages parameter
    /// </summary>
    /// <param name="dommage">Dommage that you want to affect to player</param>
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
}